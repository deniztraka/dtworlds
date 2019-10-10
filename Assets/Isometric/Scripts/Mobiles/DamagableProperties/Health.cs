using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
namespace DTWorlds.Mobiles.DamagableProperties
{
    public class Health : BaseDamagableProperty
    {
        public Health(BaseMobile mobile) : base(mobile)
        {
            MaxValue = mobile.Strength.Value * 10;
            CurrentValue = MaxValue;

            Mobile.Strength.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(OnAfterValueStrengthChanged);
        }

        private void OnAfterValueStrengthChanged()
        {
            MaxValue = Mobile.Strength.Value * 10;
        }
    }
}