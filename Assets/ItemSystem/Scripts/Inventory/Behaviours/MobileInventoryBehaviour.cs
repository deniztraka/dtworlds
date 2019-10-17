using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Inventory.Models;
using DTWorlds.UnityBehaviours;
using UnityEngine;

namespace DTWorlds.Items.Inventory.Behaviours
{
    public class MobileInventoryBehaviour : BaseStorageBehaviour
    {

        public BaseMobileBehaviour BaseMobileBehaviour;

        protected override void Start()
        {
            base.Start();
            
            if (Storage == null && BaseMobileBehaviour != null)
            {
                Storage = BaseMobileBehaviour.Mobile.Inventory;
                Storage.OnAfterItemAdded += new BaseStorage.StorageEventHandler(OnAfterItemAdded);
            }
        }
    }
}