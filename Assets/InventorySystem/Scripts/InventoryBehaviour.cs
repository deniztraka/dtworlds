using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryBehaviour : MonoBehaviour
    {
        public GameObject InventorySlotPrefab;
        public GameObject ExampleInventoryItemPrefab;
        private int sizeX = 5;
        private int sizeY = 7;
        private bool isInitialized;
        public GameObject[][] SlotGrid;

        // Start is called before the first frame update
        public virtual void Start()
        {
            Initialize();
        }

        protected void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            SlotGrid = new GameObject[sizeX][];

            for (int x = 0; x < SlotGrid.Length; x++)
            {
                if (SlotGrid[x] == null)
                {
                    SlotGrid[x] = new GameObject[sizeY];
                }
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    SlotGrid[x][y] = Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
                    var slotBehaviour = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>();
                    slotBehaviour.SlotIndex = String.Format("{0}_{1}", x, y);
                }
            }
            isInitialized = true;
        }

        public InventorySlotBehaviour GetSelectedSlot()
        {
            for (int x = 0; x < SlotGrid.Length; x++)
            {
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    if (SlotGrid[x][y] != null)
                    {
                        var slotBehaviour = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>();
                        if (slotBehaviour.IsSelected && slotBehaviour.HasItem)
                        {
                            return slotBehaviour;
                        }
                    }
                }
            }

            return null;
        }

        public InventorySlotBehaviour AddItem(ItemBehaviour item)
        {
            var emptySlot = GetEmptySlot();
            if (emptySlot == null)
            {
                return null;
            }

            emptySlot.AddItem(item.ItemInstance);

            return emptySlot;
        }

        public InventorySlotBehaviour GetEmptySlot()
        {
            if (!isInitialized)
            {
                return null;
            }

            InventorySlotBehaviour slotToBeReturned = null;

            for (int x = 0; x < SlotGrid.Length; x++)
            {
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    if (SlotGrid[x][y] != null)
                    {
                        var inventoryItem = SlotGrid[x][y].GetComponentInChildren<DragAndDropItem>();
                        if (inventoryItem == null)
                        {
                            slotToBeReturned = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>();
                            break;
                        }
                    }
                }

                if (slotToBeReturned != null)
                {
                    break;
                }
            }

            return slotToBeReturned;
        }

        internal void DropSelectedItem()
        {
            var selectedSlot = GetSelectedSlot();
            if (selectedSlot != null)
            {
                var inventoryItem = selectedSlot.GetInventoryItem();
                if (inventoryItem != null)
                {
                    var createdGameObject = GameObject.Instantiate(inventoryItem.ItemInstance.ItemTemplate.ItemPrefab, GameObject.FindWithTag("Player").transform.position, Quaternion.identity);
                    var itemBehaviour = createdGameObject.GetComponent<ItemBehaviour>();
                    itemBehaviour.ItemInstance.Quantity = inventoryItem.ItemInstance.Quantity;

                    selectedSlot.DeleteItem();
                }
            }


            // var vicinityBehaviour = targetSlot.GetComponentInParent<VicinityPackBehaviour>();
            // vicinityBehaviour.AddItemRelation(targetSlot.SlotIndex, itemBehaviour.gameObject);
        }
    }
}