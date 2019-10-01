using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem.Interfaces;
using UnityEngine.UI;
using DTWorlds.Items;

namespace InventorySystem
{

    public class InventorySlotBehaviour : MonoBehaviour
    {
        public string SlotIndex;

        private DragAndDropCell dragAndDropCell;

        public GameObject InventoryItemPrefab;

        public bool HasItem = false;

        public delegate void InventorySlotEventHandler(ItemInstance inventoryItem);
        public event InventorySlotEventHandler OnSelected;
        public event InventorySlotEventHandler OnUnSelected;

        // Start is called before the first frame update
        void Awake()
        {
            dragAndDropCell = GetComponent<DragAndDropCell>();
        }

        // Update is called once per frame
        public void OnClick()
        {
            dragAndDropCell.ToggleSelected();
            if (dragAndDropCell.IsSelected)
            {
                var item = GetInventoryItem();
                if (item != null)
                {
                    gameObject.SendMessageUpwards("OnInventoryItemSelected", item, SendMessageOptions.DontRequireReceiver);
                }
            }
            // }
            // else
            // {
            //     gameObject.SendMessageUpwards("OnInventoryItemUnSelected", null, SendMessageOptions.DontRequireReceiver);
            // }
        }

        public void AddItem(ItemInstance item)
        {
            if (item != null)
            {
                var inventoryItem = Instantiate(InventoryItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                var inventoryItemBehaviour = inventoryItem.GetComponent<InventoryItemBehaviour>();
                inventoryItemBehaviour.ItemInstance = item;
                inventoryItemBehaviour.SetItem();
                dragAndDropCell.AddItem(inventoryItem.GetComponent<DragAndDropItem>());
                HasItem = true;
            }         
        }

        public InventoryItemBehaviour GetInventoryItem()
        {
            var dragAndDropItem = dragAndDropCell.GetItem();
            if (dragAndDropItem == null)
            {
                return null;
            }
            
            return dragAndDropItem.GetComponent<InventoryItemBehaviour>();
        }

        public void DeleteItem(){
            var inventoryItem = GetComponentInChildren<InventoryItemBehaviour>();
            if(inventoryItem != null){
                inventoryItem.ItemInstance = null;
                Destroy(inventoryItem.gameObject);
            }
        }
    }
}