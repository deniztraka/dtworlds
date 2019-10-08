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
        private const string statsTextFormat = "{2}:<b>{1}{0}</b>";

        public EquipmentType EquipmentType;

        public int ArmorBonus;

        public int DexterityBonus;

        public int StrengthBonus;


        public virtual void SetModifiers(BaseMobile mobile)
        {
            if (ArmorBonus != 0)
                mobile.Armor.AddModifier(new StatModifier(ArmorBonus, StatModType.Flat, this));

            if (DexterityBonus != 0)
                mobile.Dexterity.AddModifier(new StatModifier(DexterityBonus, StatModType.Flat, this));

            if (StrengthBonus != 0)
                mobile.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        }

        public void RemoveModifiers(BaseMobile mobile)
        {
            mobile.Armor.RemoveAllModifiersFromSource(this);
        }

        public override string GetStatsText()
        {

            var sb = new StringBuilder();
            if (ArmorBonus != 0)
            {
                sb.Append(" ");
                sb.AppendFormat(statsTextFormat,  ArmorBonus, ArmorBonus > 0 ? "+":"-","AR");
                sb.Append(" ");
            }
            if (StrengthBonus != 0)
            {
                sb.Append(" ");
                sb.AppendFormat(statsTextFormat, StrengthBonus, StrengthBonus > 0 ? "+":"-","STR");
                sb.Append(" ");
            }
            if (DexterityBonus != 0)
            {
                sb.Append(" ");
                sb.AppendFormat(statsTextFormat, DexterityBonus, DexterityBonus > 0 ? "+":"-","DEX");
                sb.Append(" ");
            }
            return sb.ToString();
        }
    }
}
