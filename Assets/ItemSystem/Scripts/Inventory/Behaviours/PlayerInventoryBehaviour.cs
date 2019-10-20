using System;
using System.Collections;
using System.Collections.Generic;
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
                var shortPants = ItemDatabase.GetItemByName("Short Pants");
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPants, ItemQuality.Weak, 1));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPants, ItemQuality.Weak, 1));
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPants, ItemQuality.Exceptional, 3));
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPants, ItemQuality.Rare, 4));
                // Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), shortPants, ItemQuality.Legend, 99));                

                var healthPotion = ItemDatabase.GetItemByName("Health Potion");
                //Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Weak, 20));                
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Weak, 1));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Weak, 1));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Exceptional, 1000));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Rare, 3));
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Legend, 2));

            }
        }
    }
}