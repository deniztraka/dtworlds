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
        public SelectedItemPanelBehaviour SelectedItemPanelBehaviour;

        public CharacterEquipmentSlotBehaviour ChestSlot;
        public CharacterEquipmentSlotBehaviour LeftHandSlot;
        public CharacterEquipmentSlotBehaviour RightHandSlot;
        public CharacterEquipmentSlotBehaviour LegsSlot;
        public CharacterEquipmentSlotBehaviour BootsSlot;



        internal void SetSlot(ItemInstance itemInstance)
        {
            var baseEquipment = itemInstance.ItemTemplate as BaseEquipment;
            switch (baseEquipment.EquipmentType)
            {
                case EquipmentType.Legs:
                    LegsSlot.SetItem(itemInstance);
                    break;
                case EquipmentType.Boots:
                    BootsSlot.SetItem(itemInstance);
                    break;
                case EquipmentType.Chest:
                    ChestSlot.SetItem(itemInstance);
                    break;
                case EquipmentType.RightHand:
                    RightHandSlot.SetItem(itemInstance);
                    break;
                case EquipmentType.LeftHand:
                    LeftHandSlot.SetItem(itemInstance);
                    break;
            }
        }

        public void OnSlotSelected(CharacterEquipmentSlotBehaviour slot)
        {
            SelectedItemPanelBehaviour.gameObject.SetActive(true);
            var itemInstance = slot.GetItemInstance();
            SelectedItemPanelBehaviour.SetPanel(itemInstance);

            if (slot.GetInstanceID().Equals(LegsSlot.GetInstanceID()))
            {
                ChestSlot.SetSelected(false);
                BootsSlot.SetSelected(false);
                RightHandSlot.SetSelected(false);
                LeftHandSlot.SetSelected(false);
            }
            else if (slot.GetInstanceID().Equals(BootsSlot.GetInstanceID()))
            {
                ChestSlot.SetSelected(false);
                LegsSlot.SetSelected(false);
                RightHandSlot.SetSelected(false);
                LeftHandSlot.SetSelected(false);
            }
            else if (slot.GetInstanceID().Equals(ChestSlot.GetInstanceID()))
            {
                BootsSlot.SetSelected(false);
                LegsSlot.SetSelected(false);
                RightHandSlot.SetSelected(false);
                LeftHandSlot.SetSelected(false);
            }
            else if (slot.GetInstanceID().Equals(RightHandSlot.GetInstanceID()))
            {
                BootsSlot.SetSelected(false);
                LegsSlot.SetSelected(false);
                ChestSlot.SetSelected(false);
                LeftHandSlot.SetSelected(false);
            }
            else if (slot.GetInstanceID().Equals(LeftHandSlot.GetInstanceID()))
            {
                BootsSlot.SetSelected(false);
                LegsSlot.SetSelected(false);
                ChestSlot.SetSelected(false);
                RightHandSlot.SetSelected(false);
            }

            MobileInventoryBehaviour.DisableButtons();

            MobileInventoryBehaviour.ClearSelection();
        }

        public void OnSlotUnSelected(CharacterEquipmentSlotBehaviour slot)
        {

            SelectedItemPanelBehaviour.gameObject.SetActive(false);
            SelectedItemPanelBehaviour.SetPanel(null);

            //MobileInventoryBehaviour.ClearSelection();
        }
    }
}