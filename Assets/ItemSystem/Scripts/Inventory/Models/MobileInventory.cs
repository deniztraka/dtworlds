using System.Collections;
using System.Collections.Generic;
using DTWorlds.Mobiles;
using UnityEngine;

namespace DTWorlds.Items.Inventory.Models
{
    public class MobileInventory : BaseStorage
    {
        private BaseMobile mobile;
        public BaseMobile Mobile
        {
            get { return mobile; }
        }

        public MobileInventory(BaseMobile mobile)
        {
            this.mobile = mobile;
            MaxWeight = (float)mobile.Strength.Value * 10;
        }
    }
}