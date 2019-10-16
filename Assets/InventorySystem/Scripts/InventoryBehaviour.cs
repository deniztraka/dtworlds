﻿using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items;
using DTWorlds.Items.Behaviours;
using InventorySystem.UI;
using UnityEngine;
using static DragAndDropCell;

namespace InventorySystem
{
    public class InventoryBehaviour : MonoBehaviour
    {
        public GameObject InventorySlotPrefab;
        public GameObject ExampleInventoryItemPrefab;
        public int SizeX = 5;
        public int SizeY = 7;
        private bool isInitialized;
        public GameObject[][] SlotGrid;

        // Start is called before the first frame update
        public virtual void Start()
        {
            Initialize();
        }

        public void OnInventoryItemSelected(SelectedItemMessage msg)
        {
            for (int x = 0; x < SlotGrid.Length; x++)
            {
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    var inventorySlotBehaviour = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>();
                    if (!msg.InventoryItemBehaviour.GetComponentInParent<InventorySlotBehaviour>().SlotIndex.Equals(inventorySlotBehaviour.SlotIndex))
                    {
                        inventorySlotBehaviour.SetSelected(false);
                    }
                }
            }
        }

        public void Stack(InventoryItemBehaviour itemWithSameType, int quantity)
        {
            itemWithSameType.ItemInstance.Quantity += quantity;
        }

        public InventoryItemBehaviour GetItemByTemplate(BaseItem itemTemplate)
        {
            for (int x = 0; x < SlotGrid.Length; x++)
            {
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    var inventoryItemBehaviour = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>().GetInventoryItem();
                    if (inventoryItemBehaviour != null && inventoryItemBehaviour.ItemInstance.ItemTemplate.Equals(itemTemplate))
                    {
                        return inventoryItemBehaviour;
                    }
                }
            }

            return null;
        }

        public InventoryItemBehaviour GetSameTypeItem(ItemInstance itemInstance)
        {
            for (int x = 0; x < SlotGrid.Length; x++)
            {
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    var inventoryItemBehaviour = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>().GetInventoryItem();
                    if (inventoryItemBehaviour != null &&
                    inventoryItemBehaviour.ItemInstance.ItemTemplate.GetInstanceID().Equals(itemInstance.ItemTemplate.GetInstanceID()) &&
                    inventoryItemBehaviour.ItemInstance.Quality == itemInstance.Quality)
                    {
                        return inventoryItemBehaviour;
                    }
                }
            }

            return null;
        }

        public InventoryItemBehaviour GetItemById(string uniqueIdentifier)
        {
            for (int x = 0; x < SlotGrid.Length; x++)
            {
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    var inventoryItemBehaviour = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>().GetInventoryItem();
                    if (inventoryItemBehaviour != null && inventoryItemBehaviour.ItemInstance.Id.Equals(uniqueIdentifier))
                    {
                        return inventoryItemBehaviour;
                    }
                }
            }

            return null;
        }

        protected void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            SlotGrid = new GameObject[SizeX][];

            for (int x = 0; x < SlotGrid.Length; x++)
            {
                if (SlotGrid[x] == null)
                {
                    SlotGrid[x] = new GameObject[SizeY];
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

        public virtual InventorySlotBehaviour AddItem(ItemBehaviour item)
        {
            var itemFound = GetStackableItem(item.ItemInstance);
            if (itemFound != null)
            {
                var stackSlot = itemFound.GetComponentInParent<InventorySlotBehaviour>();
                var stackResult = stackSlot.Stack(item.ItemInstance.Quantity);
                if (stackResult)
                {
                    return stackSlot;
                }
            }

            //empty slot found
            var emptySlot = GetEmptySlot();
            if (emptySlot == null)
            {
                return null;
            }

            emptySlot.AddItem(item.ItemInstance);

            return emptySlot;
        }

        private InventoryItemBehaviour GetStackableItem(ItemInstance itemInstance)
        {
            for (int x = 0; x < SlotGrid.Length; x++)
            {
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    var inventoryItemBehaviour = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>().GetInventoryItem();
                    if (inventoryItemBehaviour != null &&
                    inventoryItemBehaviour.ItemInstance.ItemTemplate.GetInstanceID().Equals(itemInstance.ItemTemplate.GetInstanceID()) &&
                    inventoryItemBehaviour.ItemInstance.Quality == itemInstance.Quality &&
                    inventoryItemBehaviour.ItemInstance.Quantity + itemInstance.Quantity <= inventoryItemBehaviour.ItemInstance.ItemTemplate.MaxStack)
                    {
                        return inventoryItemBehaviour;
                    }
                }
            }

            return null;
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
                    itemBehaviour.ItemInstance = inventoryItem.ItemInstance;

                    selectedSlot.DeleteItem();
                }
            }


            // var vicinityBehaviour = targetSlot.GetComponentInParent<VicinityPackBehaviour>();
            // vicinityBehaviour.AddItemRelation(targetSlot.SlotIndex, itemBehaviour.gameObject);
        }
    }
}