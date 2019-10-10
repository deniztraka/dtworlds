using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
namespace DTWorlds.Mobiles.DamagableProperties
{
    public class Energy : BaseDamagableProperty
    {
        public Energy(BaseMobile mobile) : base(mobile)
        {
            MaxValue = mobile.Dexterity.Value * 10;
            CurrentValue = MaxValue;

            Mobile.Dexterity.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(OnAfterDexterityChanged);
        }

        private void OnAfterDexterityChanged()
        {
            MaxValue = (Mobile.Dexterity.Value * 10);
        }
    }
}