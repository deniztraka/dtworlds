using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DTWorlds.Mobiles;
using DTWorlds.UnityBehaviours;
using Kryz.CharacterStats;
using UnityEngine;

namespace DTWorlds.Items.Equipments
{
    public enum EquipmentType
    {
        Generic,
        RightHand,
        LeftHand,
        Chest,
        Legs,
        Boots
    }

    public abstract class BaseEquipment : BaseItem
    {
        public EquipmentType EquipmentType;

        public ItemBonus ArmorBonus;

        public ItemBonus DexterityBonus;

        public override List<ItemBonus> GetBonusList()
        {
            return new List<ItemBonus>() { ArmorBonus, DexterityBonus };
        }
    }
}
