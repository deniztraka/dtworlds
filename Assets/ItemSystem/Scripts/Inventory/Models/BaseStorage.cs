using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DTWorlds.Items.Inventory.Models
{
    public abstract class BaseStorage
    {
        public delegate void StorageEventHandler(ItemInstance item);
        public event StorageEventHandler OnAfterItemAdded;
        public event StorageEventHandler OnAfterItemDeleted;

        private List<ItemInstance> itemList;
        private float maxWeight;
        public float MaxWeight
        {
            get { return maxWeight; }
            set { maxWeight = value; }
        }

        public BaseStorage()
        {
            itemList = new List<ItemInstance>();
        }

        public List<ItemInstance> ItemList
        {
            get { return itemList; }
            set { itemList = value; }
        }

        public virtual ItemInstance GetItemById(string id)
        {
            return itemList.Find(i => i.Id.Equals(id));
        }

        public virtual void AddItem(ItemInstance item)
        {
            itemList.Add(item);
            if (OnAfterItemAdded != null)
            {
                OnAfterItemAdded(item);
            }
        }

        public virtual void Update(ItemInstance item)
        {
            var toUpdate = itemList.First(i => i.Id.Equals(item.Id));
            toUpdate.Quality = item.Quality;
            toUpdate.Quantity = item.Quantity;
        }

        public virtual void Delete(ItemInstance item){
            itemList.Remove(item);
            if (OnAfterItemDeleted != null)
            {
                OnAfterItemDeleted(item);
            }
        }
    }
}