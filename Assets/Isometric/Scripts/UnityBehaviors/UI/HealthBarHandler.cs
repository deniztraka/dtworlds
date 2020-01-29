using DTWorlds.Interfaces;
using DTWorlds.Mobiles;
using UnityEngine;

namespace DTWorlds.UnityBehaviours.UI
{
    public class HealthBarHandler : CharacterBarsHandler
    {

        public BaseMobileBehaviour MobileBehaviour;
        void Start()
        {
            DamagablePropertyBar = gameObject.GetComponentInChildren<SimpleHealthBar>();
            DamagableProperty = MobileBehaviour.Mobile.Health;

            MobileBehaviour.Mobile.Health.OnAfterValueChangedEvent += new Mobiles.DamagableProperties.Health.DamagablePropertyValueChangedHandler(this.OnAfterValueChanged);
        }
    }
}