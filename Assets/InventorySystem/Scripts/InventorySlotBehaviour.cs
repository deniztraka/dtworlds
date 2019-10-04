using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem.Interfaces;
using UnityEngine.UI;
using DTWorlds.Items;
using static DragAndDropCell;
using System;
using InventorySystem.UI;
using DTWorlds.Items.Behaviours;

namespace InventorySystem
{


    public class InventorySlotBehaviour : MonoBehaviour
    {
        public string SlotIndex;

        private DragAndDropCell dragAndDropCell;

        public GameObject InventoryItemPrefab;

        public bool HasItem = false;

        public bool IsSelected = false;

        //public delegate void InventorySlotEventHandler(ItemInstance inventoryItem);
        //public event InventorySlotEventHandler OnSelected;
        //public event InventorySlotEventHandler OnUnSelected;

        // Start is called before the first frame update
        void Awake()
        {
            dragAndDropCell = GetComponent<DragAndDropCell>();
        }

        public void OnSimpleDragAndDropEvent(DropEventDescriptor desc)
        {
            gameObject.SendMessageUpwards("OnInventoryItemUnSelected", null, SendMessageOptions.DontRequireReceiver);

            var sourceSlot = desc.sourceCell.GetComponent<InventorySlotBehaviour>();
            sourceSlot.Refresh();

            var targetSlot = desc.destinationCell.GetComponent<InventorySlotBehaviour>();
            targetSlot.Refresh();

            var sourceTypeName = sourceSlot.GetComponentInParent<InventoryBehaviour>().GetType().Name;
            var targetTypeName = desc.item.GetComponentInParent<InventoryBehaviour>().GetType().Name;

            // Debug.Log(sourceSlot.GetComponentInParent<InventoryBehaviour>().GetType().Name + "__" +
            //     desc.item.GetComponentInParent<InventoryBehaviour>().GetType().Name);

            if (sourceTypeName.Equals("VicinityPackBehaviour") && targetTypeName.Equals("InventoryBehaviour"))
            {
                Debug.Log("pickedup from floor");
                var vicinityBehaviour = sourceSlot.GetComponentInParent<VicinityPackBehaviour>();
                vicinityBehaviour.DeleteRelatedItem(sourceSlot.SlotIndex);
            }
            else if (sourceTypeName.Equals("InventoryBehaviour") && targetTypeName.Equals("VicinityPackBehaviour"))
            {
                var draggedItemInstance = desc.item.GetComponentInParent<InventoryItemBehaviour>().ItemInstance;

                var createdGameObject = GameObject.Instantiate(draggedItemInstance.ItemTemplate.ItemPrefab, GameObject.FindWithTag("Player").transform.position, Quaternion.identity);
                var itemBehaviour = createdGameObject.GetComponent<ItemBehaviour>();
                itemBehaviour.ItemInstance.Quantity = draggedItemInstance.Quantity;
                var vicinityBehaviour = targetSlot.GetComponentInParent<VicinityPackBehaviour>();
                vicinityBehaviour.AddItemRelation(targetSlot.SlotIndex, itemBehaviour.gameObject);
            }
        }

        private void Refresh()
        {
            var inventoryItem = GetInventoryItem();
            HasItem = inventoryItem != null;
            IsSelected = false;
            dragAndDropCell.UpdateBackgroundState(IsSelected);
        }


        // Update is called once per frame
        public void OnClick()
        {
            if (HasItem)
            {
                var item = GetInventoryItem();

                ToggleSelected();

                var message = new SelectedItemMessage(IsSelected ? item : null, GetComponentInParent<InventoryBehaviour>().GetType() == typeof(InventoryBehaviour));
                gameObject.SendMessageUpwards(IsSelected ? "OnInventoryItemSelected" : "OnInventoryItemUnSelected", message, SendMessageOptions.DontRequireReceiver);
            }
        }

        private void ToggleSelected()
        {
            IsSelected = !IsSelected;

            dragAndDropCell.UpdateBackgroundState(IsSelected);
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

        public void DeleteItem()
        {
            var inventoryItem = GetComponentInChildren<InventoryItemBehaviour>();
            if (inventoryItem != null)
            {
                inventoryItem.ItemInstance = null;
                HasItem = false;
            }
        }
    }
}