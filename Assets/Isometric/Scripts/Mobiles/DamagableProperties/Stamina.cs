using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
namespace DTWorlds.Mobiles.DamagableProperties
{
    public class Energy : BaseDamagableProperty
    {
        public Energy(BaseMobile mobile) :base(mobile)
        {            
            CurrentValue = 100;
        }
    }
}