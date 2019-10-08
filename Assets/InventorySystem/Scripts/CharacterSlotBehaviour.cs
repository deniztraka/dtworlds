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
using DTWorlds.Items.Equipments;

namespace InventorySystem
{
    public class CharacterSlotBehaviour : InventorySlotBehaviour
    {
        public Image RelatedImage;

        public override void OnSimpleDragAndDropEvent(DropEventDescriptor desc)
        {
            base.OnSimpleDragAndDropEvent(desc);

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
                var movementAnimationHandler = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MovementAnimationHandler>();
                SetAnimation(desc.item.GetComponent<InventoryItemBehaviour>().ItemInstance.ItemTemplate as BaseEquipment);
                var tempColor = RelatedImage.color;
                tempColor.a = (float)175 / 255;
                RelatedImage.color = tempColor;
                HideSlotItemImage();
            }
        }

        public void UpdateImage()
        {
            if (HasItem)
            {
                var inventoryItem = GetInventoryItem();
                if (inventoryItem != null)
                {
                    var equippable = inventoryItem.ItemInstance.ItemTemplate.ItemPrefab.GetComponent<EquippableItemBehaviour>();
                    RelatedImage.sprite = equippable.EquippableItemSprite;
                }

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

                var tempRelatedImageColor = RelatedImage.color;
                tempRelatedImageColor.a = IsSelected ? 1 : (float)175 / 255;
                RelatedImage.color = tempRelatedImageColor;

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
            var equippableItem = inventoryItem.ItemInstance.ItemTemplate as BaseEquipment;

            RemoveAnimation(equippableItem.EquipmentType);

            var tempRelatedImageColor = RelatedImage.color;
            tempRelatedImageColor.a = 0;
            RelatedImage.color = tempRelatedImageColor;
        }

        internal void Unequip()
        {
            var playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();

            var inventoryBehaviour = playerBehaviour.InventoryBehaviour;
            var emptySlot = inventoryBehaviour.GetEmptySlot();
            if (emptySlot != null)
            {
                var inventoryItem = GetInventoryItem();
                var clonedItem = GameObject.Instantiate(inventoryItem.ItemInstance.ItemTemplate.ItemPrefab);
                var actualItem = clonedItem.GetComponent<ItemBehaviour>();
                actualItem.ItemInstance.Quantity = inventoryItem.ItemInstance.Quantity;
                inventoryBehaviour.AddItem(actualItem);

                GameObject.Destroy(actualItem.gameObject);

                var equipmentItem = inventoryItem.ItemInstance.ItemTemplate as BaseEquipment;
                equipmentItem.RemoveModifiers(playerBehaviour.Player);
                RemoveAnimation(equipmentItem.EquipmentType);
                DeleteItem();
                UpdateImage();

                var tempRelatedImageColor = RelatedImage.color;
                tempRelatedImageColor.a = 0;
                RelatedImage.color = tempRelatedImageColor;

            }
            else
            {
                DropItem();
            }
        }

        private void HideSlotItemImage()
        {
            Debug.Log(transform.name);
            var itemImage = transform.GetChild(0).GetComponent<Image>();
            var tempItemImageColor = itemImage.color;
            tempItemImageColor.a = 0;
            itemImage.color = tempItemImageColor;
        }

        internal override void SetSelected(bool selected)
        {
            base.SetSelected(selected);
            if (selected)
            {
                HideSlotItemImage();
            }else{
                 var tempRelatedImageColor = RelatedImage.color;
                tempRelatedImageColor.a = IsSelected ? 1 : (float)175 / 255;
                RelatedImage.color = tempRelatedImageColor;
            }
        }

        void SetAnimation(BaseEquipment equipment)
        {
            var movementAnimationHandler = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MovementAnimationHandler>();
            Animator animator = null;
            switch (equipment.EquipmentType)
            {
                case EquipmentType.Chest:
                    break;
                case EquipmentType.Legs:
                    animator = movementAnimationHandler.LegsSlot.GetComponentInChildren<Animator>();
                    animator.runtimeAnimatorController =
                        equipment.ItemPrefab.GetComponent<EquippableItemBehaviour>().AnimatorController;
                    break;
                case EquipmentType.Boots:
                    break;
                case EquipmentType.RightHand:
                    break;
                case EquipmentType.LeftHand:
                    break;
            }
        }

        public override void AddItem(ItemInstance item)
        {
            base.AddItem(item);

            var equipmentItem = item.ItemTemplate as BaseEquipment;
            equipmentItem.SetModifiers(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().Player);

            var tempColor = RelatedImage.color;
            tempColor.a = (float)175 / 255;
            RelatedImage.color = tempColor;

            HideSlotItemImage();
        }

        void RemoveAnimation(EquipmentType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentType.Chest:
                    break;
                case EquipmentType.Legs:
                    var movementAnimationHandler = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<MovementAnimationHandler>();
                    var animator = movementAnimationHandler.LegsSlot.GetComponentInChildren<Animator>();
                    animator.runtimeAnimatorController = null;
                    break;
                case EquipmentType.Boots:
                    break;
                case EquipmentType.RightHand:
                    break;
                case EquipmentType.LeftHand:
                    break;
            }
        }
    }
}