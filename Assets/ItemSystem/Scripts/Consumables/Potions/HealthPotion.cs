using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DTWorlds.Mobiles;
using DTWorlds.UnityBehaviours;
using Kryz.CharacterStats;
using UnityEngine;

namespace DTWorlds.Items.Consumables.Potions
{
[CreateAssetMenu(fileName = "HealthPotion.asset", menuName = "Items/Consumables/Potions/HealthPotion", order = 1)]
    public class HealthPotion : BasePotion
    {

        public override List<ItemBonus> GetBonusList()
        {
            return new List<ItemBonus>();
        }

        public override void Use(BaseMobile mobile)
        {
            Debug.Log("Item is used.");
        }
    }
}
