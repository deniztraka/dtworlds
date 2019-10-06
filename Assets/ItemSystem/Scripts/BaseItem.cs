using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.Items
{

    public abstract class BaseItem : ScriptableObject
    {
        public string ItemName;
        public Sprite Icon;
        public float Weight;
        public int MaxStack;
    

        public GameObject ItemPrefab;

        public string ItemDescription;
    }
}