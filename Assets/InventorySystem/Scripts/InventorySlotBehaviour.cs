using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem.Interfaces;
using UnityEngine.UI;

namespace InventorySystem
{

    public class InventorySlotBehaviour : MonoBehaviour
    {
        private DragAndDropCell dragAndDropCell;

        public delegate void InventorySlotEventHandler(IInventoryItem inventoryItem);
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
                var item = GetItem();
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

        public void AddItem(DragAndDropItem item)
        {
            dragAndDropCell.AddItem(item);
        }

        public IInventoryItem GetItem()
        {
            var dragAndDropItem = dragAndDropCell.GetItem();
            if (dragAndDropItem == null)
            {
                return null;
            }
            //TODO: GET Selected Item;
            return null;
        }
    }
}