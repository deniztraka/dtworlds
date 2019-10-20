using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using DTWorlds.Items.Consumables;
using DTWorlds.Items.Equipments;
using DTWorlds.Mobiles;
using Kryz.CharacterStats;
using UnityEngine;

namespace DTWorlds.Items
{


    [Serializable]
    public class ItemInstance
    {


        private bool isModifiersSet;
        private const string statsTextFormat = "{2}:<b>{1}{0}</b>";

        public string Id;
        public BaseItem ItemTemplate;
        public int Quantity;
        public ItemQuality Quality;
        public bool IsEquipped;

        public ItemInstance(string id, BaseItem itemTemplate, ItemQuality quality, int quantity)
        {
            Id = id;
            ItemTemplate = itemTemplate;
            Quality = quality;
            Quantity = quantity;
        }

        public T getCopy<T>()
        {
            return (T)this.MemberwiseClone();
        }

        private T GetField<T>(object instance, string name)
        {
            return (T)instance.GetType().GetFields().FirstOrDefault(e => typeof(T).IsAssignableFrom(e.FieldType) && e.Name == name).GetValue(instance);
        }

        public virtual void SetModifiers(BaseMobile mobile)
        {
            if (!isModifiersSet)
            {
                var bonusList = ItemTemplate.GetBonusList();
                foreach (var bonus in bonusList)
                {
                    var characterStat = GetField<CharacterStat>(mobile, bonus.ModifierName);
                    if (characterStat != null)
                    {
                        characterStat.AddModifier(new StatModifier(bonus.Value + (int)this.Quality, StatModType.Flat, this));
                    }
                }

                isModifiersSet = true;
            }
        }

        public void RemoveModifiers(BaseMobile mobile)
        {
            if (isModifiersSet)
            {
                var bonusList = ItemTemplate.GetBonusList();
                foreach (var bonus in bonusList)
                {
                    var characterStat = GetField<CharacterStat>(mobile, bonus.ModifierName);
                    if (characterStat != null)
                    {
                        characterStat.RemoveAllModifiersFromSource(this);
                    }
                }
                isModifiersSet = false;
            }
        }

        public virtual string GetStatsText(string format)
        {
            var currentFormat = !String.IsNullOrWhiteSpace(format) ? format : statsTextFormat;
            var sb = new StringBuilder();
            var bonusList = ItemTemplate.GetBonusList();
            foreach (var bonus in bonusList)
            {
                var bonusValue = GetField<ItemBonus>(ItemTemplate, bonus.Name);
                var effectedValue = bonus.Value + (int)this.Quality;

                if (effectedValue != 0)
                {
                    sb.AppendFormat(currentFormat, effectedValue, effectedValue > 0 ? "+" : "-", bonus.UIName);
                }
            }
            return sb.ToString();
        }

        internal void SetEquipped(BaseMobile mobile)
        {
            IsEquipped = true;
            SetModifiers(mobile);
        }

        internal string GetQualityText()
        {
            return String.Format("- {0} -", this.Quality.ToString());
        }

        public bool IsEquippable()
        {
            var equipment = this.ItemTemplate as BaseEquipment;
            return equipment != null;
        }

        public bool IsUsuable()
        {
            var consumable = this.ItemTemplate as BaseConsumable;
            return consumable != null;
        }
    }
}