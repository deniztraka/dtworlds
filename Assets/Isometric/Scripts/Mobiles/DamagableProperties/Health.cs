using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
namespace DTWorlds.Mobiles.DamagableProperties
{
    public class Health : BaseDamagableProperty
    {
        public Health(BaseMobile mobile) :base(mobile)
        {
            CurrentValue = 100;
        }
    }
}