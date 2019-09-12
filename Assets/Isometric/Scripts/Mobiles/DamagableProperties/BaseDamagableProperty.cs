using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
namespace DTWorlds.Mobiles.DamagableProperties
{
    public class BaseDamagableProperty : IDamagablePropertyX
    {

        private BaseMobile mobile;
        private float currentValue;
        public float MaxValue { get; }
        public float CurrentValue
        {
            get { return currentValue; }
            set
            {
                var tempValue = Mathf.Clamp(value, 0, MaxValue);
                var tempCurrentValue = currentValue;

                currentValue = tempValue;

                if (OnAfterValueChangedEvent != null)
                {
                    OnAfterValueChangedEvent(tempCurrentValue, currentValue);
                }
            }
        }

        public delegate void DamagablePropertyValueChangedHandler(float beforeValue, float afterValue);
        public event BaseDamagableProperty.DamagablePropertyValueChangedHandler OnAfterValueChangedEvent;

        public BaseDamagableProperty(BaseMobile mobile)
        {
            this.mobile = mobile;
            MaxValue = 100;
            currentValue = 100;
        }



    }
}