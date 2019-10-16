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
        private void Awake()
        {
            if (BaseStorage == null && BaseMobileBehaviour != null)
            {
                BaseStorage = new MobileInventory(BaseMobileBehaviour.Mobile);
            }
        }
    }
}