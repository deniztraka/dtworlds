

using System;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
using DTWorlds.Items.Equipments;
using DTWorlds.UnityBehaviours;
using UnityEngine;
using UnityEngine.UI;
using static Kryz.CharacterStats.CharacterStat;

namespace InventorySystem.UI
{

    public class CharacterStatsPanel : MonoBehaviour
    {
        PlayerBehaviour playerBehaviour;
        public Text ArmorValueText;
        public Text StrengthValueText;
        public Text DexterityValueText;

        public Text HealthValueText;
        public Text HungerValueText;
        public Text EnergyValueText;

        private void Start()
        {
            playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
            playerBehaviour.Mobile.Armor.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(OnArmorChanged);
            playerBehaviour.Mobile.Strength.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(OnStrengthChanged);
            playerBehaviour.Mobile.Dexterity.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(OnDexterityChanged);

            //playerBehaviour.Player.Hunger.OnAfterValueChangedEvent += new DTWorlds.Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(OnHungerChanged);
            playerBehaviour.Mobile.Health.OnAfterValueChangedEvent += new DTWorlds.Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(OnHealthChanged);
            playerBehaviour.Mobile.Energy.OnAfterValueChangedEvent += new DTWorlds.Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(OnEnergyChanged);

            OnArmorChanged();
            OnDexterityChanged();
            OnStrengthChanged();
            //OnHungerChanged((float)playerBehaviour.Player.Hunger.CurrentValue, (float)playerBehaviour.Player.Hunger.CurrentValue);
            OnEnergyChanged((float)playerBehaviour.Mobile.Energy.CurrentValue, (float)playerBehaviour.Mobile.Energy.CurrentValue);
            OnHealthChanged((float)playerBehaviour.Mobile.Health.CurrentValue, (float)playerBehaviour.Mobile.Health.CurrentValue);
        }

        private void OnHealthChanged(float beforeValue, float afterValue)
        {
            if (HealthValueText != null)
            {
                HealthValueText.text = String.Format("{0}/{1}", Math.Floor(afterValue), playerBehaviour.Mobile.Health.MaxValue);
            }
        }

        private void OnHungerChanged(float beforeValue, float afterValue)
        {
            HungerValueText.text = String.Format("{0}/{1}", Math.Floor(afterValue), playerBehaviour.Mobile.Hunger.MaxValue);
        }

        private void OnEnergyChanged(float beforeValue, float afterValue)
        {
            if (EnergyValueText != null)
            {
                EnergyValueText.text = String.Format("{0}/{1}", Math.Floor(afterValue), playerBehaviour.Mobile.Energy.MaxValue);
            }
        }

        private void OnArmorChanged()
        {
            ArmorValueText.text = String.Format("{0}", playerBehaviour.Mobile.Armor.Value);
        }

        private void OnStrengthChanged()
        {
            StrengthValueText.text = String.Format("{0}", playerBehaviour.Mobile.Strength.Value);
        }

        private void OnDexterityChanged()
        {
            DexterityValueText.text = String.Format("{0}", playerBehaviour.Mobile.Dexterity.Value);
        }
    }
}

