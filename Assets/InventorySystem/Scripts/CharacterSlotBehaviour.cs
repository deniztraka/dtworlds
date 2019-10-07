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
    public class CharacterSlotBehaviour : InventorySlotBehaviour
    {
        public Image RelatedImage;

        public override void OnSimpleDragAndDropEvent(DropEventDescriptor desc)
        {
            base.OnSimpleDragAndDropEvent(desc);

            //Debug.Log("Equipped");

            //var characterSlotBehaviour = desc.item.GetComponentInParent<CharacterSlotBehaviour>();            

            if (desc.triggerType == TriggerType.ItemAdded || desc.triggerType == TriggerType.DropEventEnd)
            {
                var inventorySlot = desc.sourceCell.GetComponentInParent<InventorySlotBehaviour>();
                var vicinityBehaviour = inventorySlot.GetComponentInParent<VicinityPackBehaviour>();
                //if it is picked from vicinity, clear relation
                if (vicinityBehaviour != null)
                {
                    vicinityBehaviour.DeleteRelatedItem(inventorySlot.SlotIndex);
                }

                UpdateImage();
            }
        }

        public void UpdateImage()
        {
            if (HasItem)
            {
                var equippable = GetInventoryItem().ItemInstance.ItemTemplate.ItemPrefab.GetComponent<EquippableItemBehaviour>();
                RelatedImage.sprite = equippable.EquippableItemSprite;

                var tempColor = RelatedImage.color;
                tempColor.a = 1;
                RelatedImage.color = tempColor;
            }
            else
            {
                RelatedImage.sprite = null;

                var tempColor = RelatedImage.color;
                tempColor.a = 0;
                RelatedImage.color = tempColor;
            }

        }

        public override void OnClick()
        {
            if (HasItem)
            {
                var item = GetInventoryItem();

                ToggleSelected();

                var message = new SelectedItemMessage(IsSelected ? item : null, false);
                gameObject.SendMessageUpwards(IsSelected ? "OnInventoryItemSelected" : "OnInventoryItemUnSelected", message, SendMessageOptions.DontRequireReceiver);
            }
        }

        internal void DropItem()
        {
            var inventoryItem = GetInventoryItem();
            var createdGameObject = GameObject.Instantiate(inventoryItem.ItemInstance.ItemTemplate.ItemPrefab, GameObject.FindWithTag("Player").transform.position, Quaternion.identity);
            var itemBehaviour = createdGameObject.GetComponent<ItemBehaviour>();
            itemBehaviour.ItemInstance.Quantity = inventoryItem.ItemInstance.Quantity;

            DeleteItem();
            UpdateImage();
        }
    }
}