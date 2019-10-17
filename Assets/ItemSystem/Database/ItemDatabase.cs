using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.Items
{
    [CreateAssetMenu(fileName = "ItemDatabase.asset", menuName = "Items/Database/ItemDatabase", order = 1)]
    public class ItemDatabase : ScriptableObject
    {
        public List<BaseItem> ItemList;

        public BaseItem GetItemByName(string name){
            return ItemList.Find(i=>i.ItemName.Equals(name));
        }
    }
}