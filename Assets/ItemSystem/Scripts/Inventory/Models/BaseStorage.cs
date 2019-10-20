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
        public event StorageEventHandler OnAfterItemUpdated;

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

        public virtual ItemInstance AddItem(ItemInstance item)
        {
            var canBeStacked = false;
            var stackables = itemList.FindAll(i =>
                i.ItemTemplate.GetInstanceID().Equals(item.ItemTemplate.GetInstanceID()) &&
                i.Quality == item.Quality &&
                (i.Quantity < item.ItemTemplate.MaxStack));

            canBeStacked = stackables.Count != 0;

            if (canBeStacked)
            {
                StackItem(item, stackables);
            }
            else
            {
                itemList.Add(item);
                if (OnAfterItemAdded != null)
                {
                    OnAfterItemAdded(item);
                }
            }

            return item;
        }

        protected virtual void StackItem(ItemInstance item, List<ItemInstance> stackables)
        {
            var stackableItem = stackables.First();

            var tempStackCount = stackableItem.Quantity + item.Quantity;
            if (tempStackCount > stackableItem.ItemTemplate.MaxStack)
            {
                stackableItem.Quantity = stackableItem.ItemTemplate.MaxStack;
                var newItemQuantity = tempStackCount - stackableItem.ItemTemplate.MaxStack;
                //Debug.Log(newItemQuantity);

                var newItem = item;
                newItem.Quantity = newItemQuantity;
                itemList.Add(newItem);
                if (OnAfterItemAdded != null)
                {
                    OnAfterItemAdded(item);
                }
            }
            else
            {
                stackableItem.Quantity += item.Quantity;
            }


            if (OnAfterItemUpdated != null)
            {
                OnAfterItemUpdated(stackableItem);
            }
        }

        public virtual void Update(ItemInstance item)
        {
            var toUpdate = itemList.First(i => i.Id.Equals(item.Id));
            toUpdate.Quality = item.Quality;
            toUpdate.Quantity = item.Quantity;

            if (OnAfterItemUpdated != null)
            {
                OnAfterItemUpdated(item);
            }
        }

        public virtual void Delete(ItemInstance item)
        {
            itemList.Remove(item);
            if (OnAfterItemDeleted != null)
            {
                OnAfterItemDeleted(item);
            }
        }

        public List<ItemInstance> GetItemsByType(ItemType itemType)
        {
            return itemList.FindAll(i => i.ItemTemplate.Type.Equals(itemType)).OrderBy(i => i.IsEquipped).ToList();
        }

        public ItemInstance GetSameTypeEquippedItem(ItemType itemType)
        {
            return itemList.FindAll(i => i.ItemTemplate.Type.Equals(itemType) && i.IsEquipped).FirstOrDefault();
        }
    }
}