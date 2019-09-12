using DTWorlds.Interfaces;
using DTWorlds.Mobiles.DamagableProperties;
using UnityEngine;

namespace DTWorlds.UnityBehaviours.UI
{
    public class CharacterBarsHandler : MonoBehaviour
    {
        public BaseDamagableProperty DamagableProperty;
        public SimpleHealthBar DamagablePropertyBar;

        // Start is called before the first frame update
        void Start()
        {
            DamagablePropertyBar = gameObject.GetComponent<SimpleHealthBar>();
            DamagableProperty.OnAfterValueChangedEvent += new Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(OnAfterValueChanged);
        }

        public virtual void OnAfterValueChanged(float beforeValue, float afterValue)
        {
            DamagablePropertyBar.UpdateBar(afterValue, DamagableProperty.MaxValue);
        }
    }
}