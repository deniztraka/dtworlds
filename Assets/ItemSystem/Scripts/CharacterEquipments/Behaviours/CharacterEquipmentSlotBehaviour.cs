using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
using DTWorlds.Items.Consumables;
using DTWorlds.Items.Equipments;
using DTWorlds.Items.Inventory.Models;
using DTWorlds.UnityBehaviours;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.Items.Inventory.CharacterEquipments.Behaviours
{
    public class CharacterEquipmentSlotBehaviour : MonoBehaviour
    {
        public Image SlotImage;
        public string ItemId;

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

        public void SetItem(ItemInstance item)
        {
            ItemId = item.IsEquipped ? item.Id : null;

            var equipment = item.ItemTemplate as BaseEquipment;
            if (item.IsEquipped)
            {
                ItemId = item.Id;

                var equippable = equipment.ItemPrefab.GetComponent<EquippableItemBehaviour>();
                SlotImage.sprite = equippable.EquippableItemSprite;

                var tempColor = SlotImage.color;
                tempColor.a = (float)175 / 255;
                SlotImage.color = tempColor;                
            }
            else
            {
                SlotImage.sprite = null;
                ItemId = null;
                var tempColor = SlotImage.color;
                tempColor.a = 0f;
                SlotImage.color = tempColor;                
            }
        }
    }
}