using DTWorlds.Interfaces;
using DTWorlds.Mobiles;
using UnityEngine;

namespace DTWorlds.UnityBehaviours.UI
{
    public class HealthBarHandler : CharacterBarsHandler
    {

        public PlayerBehaviour PlayerBehaviour;
        void Start()
        {
            DamagablePropertyBar = gameObject.GetComponentInChildren<SimpleHealthBar>();
            DamagableProperty = PlayerBehaviour.Mobile.Health;

            PlayerBehaviour.Mobile.Health.OnAfterValueChangedEvent += new Mobiles.DamagableProperties.Health.DamagablePropertyValueChangedHandler(this.OnAfterValueChanged);
        }
    }
}