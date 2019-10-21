using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Inventory.CharacterEquipments.Behaviours;
using DTWorlds.Items.Inventory.Models;
using DTWorlds.UnityBehaviours;
using UnityEngine;

namespace DTWorlds.Items.Inventory.Behaviours
{
    public class PlayerInventoryBehaviour : MobileInventoryBehaviour
    {
        public List<CharacterEquipmentSlotBehaviour> CharacterEquipmentSlotBehaviours;

        protected override void Start()
        {
            base.Start();
            if (Storage != null)
            {
                var shortPantsTemplate = ItemDatabase.GetItemByName("Short Pants");
                var equippedItem = Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPantsTemplate, ItemQuality.Weak, 1));
                Equip(equippedItem);
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPantsTemplate, ItemQuality.Regular, 1));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPantsTemplate, ItemQuality.Exceptional, 1));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPantsTemplate, ItemQuality.Rare, 1));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPantsTemplate, ItemQuality.Legend, 1));

                var healthPotionTemplate = ItemDatabase.GetItemByName("Health Potion");
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotionTemplate, ItemQuality.Weak, 20));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotionTemplate, ItemQuality.Regular, 3));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotionTemplate, ItemQuality.Exceptional, 20));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotionTemplate, ItemQuality.Rare, 99));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotionTemplate, ItemQuality.Legend, 85));

            }
        }

        public override void OnAfterSlotSelected(BaseStorageSlotBehaviour slot)
        {
            foreach (var characterEquipmentSlot in CharacterEquipmentSlotBehaviours)
            {
                characterEquipmentSlot.SetSelected(false);
            }

            base.OnAfterSlotSelected(slot);

            if (slot.IsSelected)
            {
                SelectedItemPanelBehaviour.gameObject.SetActive(true);
                SelectedItemPanelBehaviour.SetPanel(slot.GetItemInstance());
                // Debug.Log(slot.GetItemInstance().ItemTemplate.ItemName + " is selected.");
            }
            else
            {
                SelectedItemPanelBehaviour.gameObject.SetActive(false);
                SelectedItemPanelBehaviour.SetPanel(null);
                // Debug.Log(slot.GetItemInstance().ItemTemplate.ItemName + " is deselected.");
            }

        }

        public void Equip()
        {
            var selectedSlot = GetSelectedSlot();
            if (selectedSlot == null)
            {
                return;
            }
            var selectedItem = selectedSlot.GetItemInstance();
            if (selectedItem == null)
            {
                return;
            }

            Equip(selectedItem);
        }

        public void Equip(ItemInstance itemInstance)
        {

            //Check if any equipped item in same slot, then unequip it.
            var equipedItem = Storage.GetSameTypeEquippedItem(itemInstance.ItemTemplate.Type);
            if (equipedItem != null)
            {
                UnEquip(equipedItem);
            }

            //if its get this far, we have item here
            var playerBehaviour = BaseMobileBehaviour as PlayerBehaviour;
            playerBehaviour.Equip(itemInstance);
            UpdateItem(itemInstance);
            RenderProperButtons();
        }

        private void UnEquip(ItemInstance equipedItem)
        {
            if (equipedItem != null)
            {
                var playerBehaviour = BaseMobileBehaviour as PlayerBehaviour;
                playerBehaviour.Unequip(equipedItem);
                UpdateItem(equipedItem);
                RenderProperButtons();
            }
        }

        internal ItemInstance GetItemById(string itemId)
        {
            return Storage.GetItemById(itemId);
        }

        public void UnEquip()
        {
            var selectedSlot = GetSelectedSlot();
            if (selectedSlot == null)
            {
                //Debug.Log("It's not an equipment.");
                return;
            }
            var selectedItem = selectedSlot.GetItemInstance();
            if (selectedItem == null)
            {
                //Debug.Log("Selected item is null.");
                return;
            }

            //if its get this far, we have item here
            UnEquip(selectedItem);



        }
    }
}