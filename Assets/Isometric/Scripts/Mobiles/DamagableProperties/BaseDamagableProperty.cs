using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
namespace DTWorlds.Mobiles.DamagableProperties
{
    public class BaseDamagableProperty : IDamagablePropertyX
    {

        protected BaseMobile Mobile;
        private float currentValue;
        private float maxValue;

        public float MaxValue
        {
            get { return maxValue; }
            set
            {
                maxValue = value;

                //this is just for registered object updates when max value changed
                if (OnAfterValueChangedEvent != null)
                {
                    OnAfterValueChangedEvent(currentValue, currentValue);
                }
            }
        }
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
            Mobile = mobile;
        }



    }
}