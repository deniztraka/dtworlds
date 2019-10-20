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
        protected override void Start()
        {
            base.Start();
            if (Storage != null)
            {
                var shortPantsTemplate = ItemDatabase.GetItemByName("Short Pants");
                var equippedItem = Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPantsTemplate, ItemQuality.Weak, 1));
                Equip(equippedItem);
                //Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPants, ItemQuality.Weak, 1));
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPants, ItemQuality.Exceptional, 3));
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPants, ItemQuality.Rare, 4));
                //Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPants, ItemQuality.Legend, 1));

                // var healthPotion = ItemDatabase.GetItemByName("Health Potion");
                // //Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Weak, 20));                
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Weak, 1));
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Weak, 1));
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Exceptional, 1000));
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Rare, 3));
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Legend, 2));

            }
        }

        public void Equip()
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