using System.Collections;
using System.Collections.Generic;
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

        public int ArmorBonus;


        public void SetModifiers(BaseMobile mobile)
        {
            if (ArmorBonus != 0)
                mobile.Armor.AddModifier(new StatModifier(ArmorBonus, StatModType.Flat, this));
        }

        public void RemoveModifiers(BaseMobile mobile)
        {
            mobile.Armor.RemoveAllModifiersFromSource(this);
        }
    }
}
