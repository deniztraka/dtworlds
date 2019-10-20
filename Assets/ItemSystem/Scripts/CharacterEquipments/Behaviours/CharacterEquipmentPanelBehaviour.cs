using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
using DTWorlds.Items.Consumables;
using DTWorlds.Items.Equipments;
using DTWorlds.Items.Inventory.Behaviours;
using DTWorlds.Items.Inventory.Models;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.Items.Inventory.CharacterEquipments.Behaviours
{
    public class CharacterEquipmentPanelBehaviour : MonoBehaviour
    {
        public MobileInventoryBehaviour MobileInventoryBehaviour;

        public CharacterEquipmentSlotBehaviour ChestSlot;
        public CharacterEquipmentSlotBehaviour LeftHandSlot;
        public CharacterEquipmentSlotBehaviour RightHandSlot;
        public CharacterEquipmentSlotBehaviour LegsSlot;
        public CharacterEquipmentSlotBehaviour BootsSlot;

        internal void SetSlot(ItemInstance selectedItem)
        {
            var baseEquipment = selectedItem.ItemTemplate as BaseEquipment;
            switch (baseEquipment.EquipmentType)
            {
                case EquipmentType.Legs:
                    LegsSlot.SetItem(selectedItem);
                    break;
                case EquipmentType.Boots:
                    BootsSlot.SetItem(selectedItem);
                    break;
                case EquipmentType.Chest:
                    ChestSlot.SetItem(selectedItem);
                    break;
                case EquipmentType.RightHand:
                    RightHandSlot.SetItem(selectedItem);
                    break;
                case EquipmentType.LeftHand:
                    LeftHandSlot.SetItem(selectedItem);
                    break;
            }
        }
    }
}