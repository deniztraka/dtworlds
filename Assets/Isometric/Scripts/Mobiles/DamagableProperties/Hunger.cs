using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
namespace DTWorlds.Mobiles.DamagableProperties
{
    public class Hunger : BaseDamagableProperty
    {
        public Hunger(BaseMobile mobile) : base(mobile)
        {
            MaxValue = 100;
            CurrentValue = MaxValue;
        }
    }
}