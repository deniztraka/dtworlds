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

                var item = ItemDatabase.GetItemByName("Short Pants");
                Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), item, ItemQuality.Rare, 1));
            }
        }
    }
}