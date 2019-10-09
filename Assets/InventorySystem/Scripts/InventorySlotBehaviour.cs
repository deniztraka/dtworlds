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
using DTWorlds.UnityBehaviours;

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

        public virtual void OnSimpleDragAndDropEvent(DropEventDescriptor desc)
        {
            //TODO:
            //Identify every drag and drop operation source and destination slots
            //and do the required operations


            gameObject.SendMessageUpwards("OnInventoryItemUnSelected", null, SendMessageOptions.DontRequireReceiver);

            var sourceSlot = desc.sourceCell.GetComponent<InventorySlotBehaviour>();
            sourceSlot.Refresh();

            var targetSlot = desc.destinationCell.GetComponent<InventorySlotBehaviour>();
            targetSlot.Refresh();

            var inventoryComponent = sourceSlot.GetComponentInParent<InventoryBehaviour>();
            if (inventoryComponent != null)
            {


                var sourceTypeName = inventoryComponent.GetType().Name;
                var inventoryBehaviour = targetSlot.GetComponentInParent<InventoryBehaviour>();
                if (inventoryBehaviour != null)
                {
                    var targetTypeName = inventoryBehaviour.GetType().Name;



                    // Debug.Log(sourceSlot.GetComponentInParent<InventoryBehaviour>().GetType().Name + "__" +
                    //     desc.item.GetComponentInParent<InventoryBehaviour>().GetType().Name);

                    if (sourceTypeName.Equals("VicinityPackBehaviour") && targetTypeName.Equals("InventoryBehaviour"))
                    {
                        Debug.Log("Pickedup From Floor");
                        //Debug.Log("pickedup from floor");
                        var vicinityBehaviour = sourceSlot.GetComponentInParent<VicinityPackBehaviour>();
                        vicinityBehaviour.DeleteRelatedItem(sourceSlot.SlotIndex);
                    }
                    else if (sourceTypeName.Equals("InventoryBehaviour") && targetTypeName.Equals("VicinityPackBehaviour"))
                    {
                        Debug.Log("Dropped");
                        //this is drop operation
                        var draggedItemInstance = desc.item.GetComponentInParent<InventoryItemBehaviour>().ItemInstance;

                        var createdGameObject = GameObject.Instantiate(draggedItemInstance.ItemTemplate.ItemPrefab, GameObject.FindWithTag("Player").transform.position, Quaternion.identity);
                        var itemBehaviour = createdGameObject.GetComponent<ItemBehaviour>();
                        itemBehaviour.ItemInstance = draggedItemInstance;
                        var vicinityBehaviour = targetSlot.GetComponentInParent<VicinityPackBehaviour>();
                        vicinityBehaviour.AddItemRelation(targetSlot.SlotIndex, itemBehaviour.gameObject);
                    }


                }
                else if (targetSlot.GetComponent<CharacterSlotBehaviour>() != null)
                {
                    //target is character slot
                    var targetCharacterSlot = targetSlot.GetComponent<CharacterSlotBehaviour>();
                    if (desc.triggerType == TriggerType.DropRequest)
                    {
                        //if has item, uneqip it first
                        if (targetCharacterSlot.HasItem)
                        {
                            targetCharacterSlot.Unequip();
                        }
                    }
                    else if (desc.triggerType == TriggerType.DropEventEnd)
                    {
                        var inventoryItem = desc.item;
                        if (!inventoryItem.name.StartsWith("CharacterSlotItemPrefab"))
                        {
                            var characterSlotInventoryItem = GameObject.Instantiate(InventoryItemPrefab, GameObject.FindWithTag("Player").transform.position, Quaternion.identity, targetCharacterSlot.transform).GetComponent<InventoryItemBehaviour>();
                            characterSlotInventoryItem.ItemInstance = inventoryItem.GetComponent<InventoryItemBehaviour>().ItemInstance;
                            Destroy(inventoryItem.gameObject);
                        }
                        targetCharacterSlot.HideSlotItemImage();
                    }
                }

            }
        }

        internal virtual void SetSelected(bool selected)
        {
            IsSelected = selected;

            dragAndDropCell.UpdateBackgroundState(IsSelected);
        }

        private void Refresh()
        {
            var inventoryItem = GetInventoryItem();
            HasItem = inventoryItem != null;
            IsSelected = false;
            dragAndDropCell.UpdateBackgroundState(IsSelected);
        }


        // Update is called once per frame
        public virtual void OnClick()
        {
            if (HasItem)
            {
                var inventoryBehaviour = GetComponentInParent<InventoryBehaviour>();
                var item = GetInventoryItem();

                ToggleSelected();

                var message = new SelectedItemMessage(IsSelected ? item : null, inventoryBehaviour.GetType() == typeof(InventoryBehaviour));
                gameObject.SendMessageUpwards(IsSelected ? "OnInventoryItemSelected" : "OnInventoryItemUnSelected", message, SendMessageOptions.DontRequireReceiver);
            }
        }

        protected void ToggleSelected()
        {
            IsSelected = !IsSelected;
            dragAndDropCell.UpdateBackgroundState(IsSelected);
        }

        public virtual void AddItem(ItemInstance item)
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
            return GetComponentInChildren<InventoryItemBehaviour>();
        }

        public void DeleteItem()
        {
            var inventoryItem = GetComponentInChildren<InventoryItemBehaviour>();
            if (inventoryItem != null)
            {
                //inventoryItem.ItemInstance = null;
                Destroy(inventoryItem.gameObject);
                HasItem = false;
                IsSelected = false;
            }
        }
    }
}