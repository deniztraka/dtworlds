using DTWorlds.Interfaces;
using DTWorlds.Mobiles;
using UnityEngine;

namespace DTWorlds.UnityBehaviours.UI
{
    public class HungerBarHandler : CharacterBarsHandler
    {

        public PlayerBehaviour PlayerBehaviour;
        void Start()
        {
            DamagablePropertyBar = gameObject.GetComponentInChildren<SimpleHealthBar>();
            DamagableProperty = PlayerBehaviour.Mobile.Hunger;

            PlayerBehaviour.Mobile.Hunger.OnAfterValueChangedEvent += new Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(this.OnAfterValueChanged);
        }
    }
}