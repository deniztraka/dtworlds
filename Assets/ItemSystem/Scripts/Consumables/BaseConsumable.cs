using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DTWorlds.Mobiles;
using DTWorlds.UnityBehaviours;
using Kryz.CharacterStats;
using UnityEngine;

namespace DTWorlds.Items.Consumables
{

    public abstract class BaseConsumable : BaseItem
    {
        public abstract bool Consume(ItemInstance ıtemInstance, BaseMobile mobile);
    }
}
