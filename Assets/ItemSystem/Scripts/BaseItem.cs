using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.Items
{
    public enum ItemType        //all ItemTypes...you can add some 
    {
        None = 0,
        Weapon = 1,
        Consumable = 2,
        Quest = 3,
        Head = 4,
        Shoe = 5,
        Chest = 6,
        Trouser = 7,
        Earrings = 8,
        Necklace = 9,
        Ring = 10,
        Hands = 11,
        Blueprint = 12,
        Ammo = 13

    }

    public abstract class BaseItem : ScriptableObject
    {
        public string ItemName;
        public Sprite Icon;
        public float Weight;
        public int MaxStack;
        
        public ItemType Type;   

        public GameObject ItemPrefab;
    }
}