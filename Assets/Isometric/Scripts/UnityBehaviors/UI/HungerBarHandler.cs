using DTWorlds.Interfaces;
using DTWorlds.Mobiles;
using UnityEngine;

namespace DTWorlds.UnityBehaviours.UI
{
    public class HungerBarHandler : CharacterBarsHandler
    {

        public BaseMobileBehaviour BaseMobileBehaviour;
        void Start()
        {
            DamagablePropertyBar = gameObject.GetComponentInChildren<SimpleHealthBar>();
            DamagableProperty = BaseMobileBehaviour.Mobile.Hunger;

            BaseMobileBehaviour.Mobile.Hunger.OnAfterValueChangedEvent += new Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(this.OnAfterValueChanged);
        }
    }
}