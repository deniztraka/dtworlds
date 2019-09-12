using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
namespace DTWorlds.Mobiles.DamagableProperties
{
    public class Stamina : BaseDamagableProperty
    {
        public Stamina(BaseMobile mobile) :base(mobile)
        {            
            CurrentValue = 100;
        }
    }
}