

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
        public Text ArmorText;

        private void Start()
        {
            playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
            playerBehaviour.Player.Armor.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(OnArmorChanged);

            OnArmorChanged();
        }

        private void OnArmorChanged()
        {
            ArmorText.text = String.Format("{0}", playerBehaviour.Player.Armor.Value);
        }
    }
}

