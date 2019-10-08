using DTWorlds.Interfaces;
using DTWorlds.Mobiles;
using UnityEngine;

namespace DTWorlds.UnityBehaviours.UI
{
    public class StaminaBarHandler : CharacterBarsHandler
    {

        public PlayerBehaviour PlayerBehaviour;
        void Start()
        {
            DamagablePropertyBar = gameObject.GetComponentInChildren<SimpleHealthBar>();
            DamagableProperty = PlayerBehaviour.Player.Energy;

            PlayerBehaviour.Player.Energy.OnAfterValueChangedEvent += new Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(this.OnAfterValueChanged);
        }
    }
}