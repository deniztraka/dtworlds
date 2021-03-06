﻿using System;
using System.Collections;
using System.Collections.Generic;
using Kryz.CharacterStats;
using UnityEngine;

namespace DTWorlds.Items
{

    public enum ItemQuality
    {
        Weak = 0,
        Regular = 1,
        Exceptional = 2,
        Rare = 3,
        Legend = 4
    }

    public enum ItemType
    {
        Weapon = 0,
        Equipment = 1,
        Potion = 2,
        Food = 3,
        Misc = 4
    }


    public abstract class BaseItem : ScriptableObject
    {
        public string ItemName;
        public Sprite Icon;
        public float Weight;
        public int MaxStack;
        public ItemQuality Quality;
        public GameObject ItemPrefab;
        public string ItemDescription;
        public string ItemLongDescription;
        public ItemType Type;
        public abstract List<ItemBonus> GetBonusList();
    }
}