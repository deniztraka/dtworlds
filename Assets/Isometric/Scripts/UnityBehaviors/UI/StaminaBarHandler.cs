using DTWorlds.Interfaces;
using DTWorlds.Mobiles;
using UnityEngine;

namespace DTWorlds.UnityBehaviours.UI
{
    public class StaminaBarHandler : CharacterBarsHandler
    {

        public BaseMobileBehaviour BaseMobileBehaviour;
        void Start()
        {
            DamagablePropertyBar = gameObject.GetComponentInChildren<SimpleHealthBar>();
            DamagableProperty = BaseMobileBehaviour.Mobile.Energy;

            BaseMobileBehaviour.Mobile.Energy.OnAfterValueChangedEvent += new Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(this.OnAfterValueChanged);
        }
    }
}