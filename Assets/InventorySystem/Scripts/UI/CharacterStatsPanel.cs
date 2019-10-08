

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
            playerBehaviour.Player.Armor.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(OnArmorChanged);
            playerBehaviour.Player.Strength.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(OnStrengthChanged);
            playerBehaviour.Player.Dexterity.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(OnDexterityChanged);

            //playerBehaviour.Player.Hunger.OnAfterValueChangedEvent += new DTWorlds.Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(OnHungerChanged);
            playerBehaviour.Player.Health.OnAfterValueChangedEvent += new DTWorlds.Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(OnHealthChanged);
            playerBehaviour.Player.Energy.OnAfterValueChangedEvent += new DTWorlds.Mobiles.DamagableProperties.BaseDamagableProperty.DamagablePropertyValueChangedHandler(OnEnergyChanged);

            OnArmorChanged();
            OnDexterityChanged();
            OnStrengthChanged();
            //OnHungerChanged((float)playerBehaviour.Player.Hunger.CurrentValue, (float)playerBehaviour.Player.Hunger.CurrentValue);
            OnEnergyChanged((float)playerBehaviour.Player.Energy.CurrentValue, (float)playerBehaviour.Player.Energy.CurrentValue);
            OnHealthChanged((float)playerBehaviour.Player.Health.CurrentValue, (float)playerBehaviour.Player.Health.CurrentValue);
        }

        private void OnHealthChanged(float beforeValue, float afterValue)
        {
            HealthValueText.text = String.Format("{0}", Math.Floor(afterValue));
        }

        private void OnHungerChanged(float beforeValue, float afterValue)
        {
            HungerValueText.text = String.Format("{0}", Math.Floor(afterValue));
        }

        private void OnEnergyChanged(float beforeValue, float afterValue)
        {
            EnergyValueText.text = String.Format("{0}", Math.Floor(afterValue));
        }

        private void OnArmorChanged()
        {
            ArmorValueText.text = String.Format("{0}", playerBehaviour.Player.Armor.Value);
        }

        private void OnStrengthChanged()
        {
            StrengthValueText.text = String.Format("{0}", playerBehaviour.Player.Strength.Value);
        }

        private void OnDexterityChanged()
        {
            DexterityValueText.text = String.Format("{0}", playerBehaviour.Player.Dexterity.Value);
        }
    }
}

