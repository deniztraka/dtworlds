using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
using DTWorlds.Items.Consumables;
using DTWorlds.Items.Inventory.Models;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.Items.Inventory.Behaviours
{
    public abstract class BaseStorageBehaviour : MonoBehaviour
    {
        public BaseStorageSlotBehaviour InventorySlotPrefab;
        public ItemDatabase ItemDatabase;

        private BaseStorage baseStorage;
        public BaseStorage Storage
        {
            get { return baseStorage; }
            set { baseStorage = value; }
        }

        public Button EquipButton;
        public Button DropButton;
        public Button StoreButton;
        public Button UseButton;

        protected virtual void Start()
        {


        }



        private void AttachSlotEvents(BaseStorageSlotBehaviour slot)
        {
            slot.OnAfterSelected += new BaseStorageSlotBehaviour.StorageSlotEventHandler(OnAfterSlotSelected);
        }

        /*
            Make every other slot unselected after the coming slot in parameter is selected.
         */
        public virtual void OnAfterSlotSelected(BaseStorageSlotBehaviour slot)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                var childSlot = transform.GetChild(i).GetComponent<BaseStorageSlotBehaviour>();
                if (!childSlot.GetItemInstance().Id.Equals(slot.GetItemInstance().Id))
                {
                    childSlot.SetSelected(false);
                }
            }

            RenderProperButtons();
        }

        private void SetButton(Button button, bool active, bool interactable)
        {
            if (button != null)
            {
                button.gameObject.SetActive(active);
                button.interactable = interactable;
            }
        }

        private void RenderProperButtons()
        {
            var selectedSlot = GetSelectedSlot();
            if (selectedSlot != null)
            {
                var selectedItemInstance = selectedSlot.GetItemInstance();

                SetButton(EquipButton, true, selectedItemInstance.IsEquippable());
                SetButton(UseButton, true, selectedItemInstance.IsUsuable());
                SetButton(DropButton, true, true);
            }
            else
            {
                //nothing is selected, make all buttons disabled.
                SetButton(EquipButton, true, false);
                SetButton(DropButton, true, false);
                SetButton(UseButton, true, false);

                //TODO: Implement store button after storage items are implemented
                SetButton(StoreButton, false, false);
            }
        }

        public BaseStorageSlotBehaviour GetSelectedSlot()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var childSlot = transform.GetChild(i).GetComponent<BaseStorageSlotBehaviour>();
                if (childSlot.IsSelected)
                {
                    return childSlot;
                }
            }

            return null;
        }

        private List<ItemInstance> FilterByType(ItemType type)
        {
            return Storage.GetItemsByType(type);
        }

        private void AddItems(List<ItemInstance> items)
        {
            var itemArray = items.ToArray();
            for (int i = 0; i < itemArray.Length; i++)
            {
                AddItem(itemArray[i]);
            }
        }

        public void FilterByEquipment()
        {
            ClearSlots();
            var items = FilterByType(ItemType.Equipment);
            OnAfterItemsAdded(items);
        }

        public void FilterByWeapons()
        {
            ClearSlots();
            var items = FilterByType(ItemType.Weapon);
            OnAfterItemsAdded(items);
        }

        public void FilterByPotions()
        {
            ClearSlots();
            var items = FilterByType(ItemType.Potion);
            OnAfterItemsAdded(items);
        }

        public void FilterByFood()
        {
            ClearSlots();
            var items = FilterByType(ItemType.Food);
            OnAfterItemsAdded(items);
        }

        public void FilterByMisc()
        {
            ClearSlots();
            var items = FilterByType(ItemType.Misc);
            OnAfterItemsAdded(items);
        }

        public void ListAll()
        {
            ClearSlots();
            OnAfterItemsAdded(Storage.ItemList);
        }

        public void TestButton()
        {
            var healthPotion = ItemDatabase.GetItemByName("Health Potion");
            Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Weak, 3));
            Storage.AddItem(new ItemInstance(Guid.NewGuid().ToString(), healthPotion, ItemQuality.Regular, 1));
        }

        public void UpdateItem(ItemInstance item){
            Storage.Update(item);
        }

        private void ClearSlots()
        {
            Transform[] childArray = new Transform[transform.childCount];
            var index = 0;
            foreach (Transform child in transform)
            {
                childArray[index] = child;
                index++;
            }
            transform.DetachChildren();

            for (int i = 0; i < childArray.Length; i++)
            {

                GameObject.Destroy(childArray[i].gameObject);
            }
        }



        public virtual void AddItem(ItemInstance item)
        {
            Storage.AddItem(item);
        }

        public virtual void Use()
        {
            var selectedSlot = GetSelectedSlot();
            if (selectedSlot != null)
            {
                var itemInstance = selectedSlot.GetItemInstance();
                if (itemInstance.IsUsuable())
                {
                    var consumable = itemInstance.ItemTemplate as BaseConsumable;
                    consumable.Consume(itemInstance, null);
                }
            }
        }

        public virtual void Equip()
        {

        }

        public virtual void Store()
        {

        }

        public virtual void Drop()
        {
            var selectedSlot = GetSelectedSlot();
            if (selectedSlot != null)
            {
                Storage.Delete(selectedSlot.GetItemInstance());
                Destroy(selectedSlot.gameObject);

            }
        }

        public virtual void OnAfterItemsAdded(List<ItemInstance> items)
        {
            foreach (var item in items)
            {
                OnAfterItemAdded(item);
            }
        }

        protected virtual void OnAfterItemAdded(ItemInstance item)
        {
            var instantiatedSlot = Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, this.transform) as BaseStorageSlotBehaviour;
            instantiatedSlot.AddItem(item);
            AttachSlotEvents(instantiatedSlot);

            RefreshViewportHeight();
        }

        protected virtual void OnAfterItemDeleted(ItemInstance item)
        {
            var createdGameObject = Instantiate(item.ItemTemplate.ItemPrefab, transform.position, Quaternion.identity);
            var itemBehaviour = createdGameObject.GetComponent<ItemBehaviour>();
            itemBehaviour.ItemInstance = item;

            RefreshViewportHeight();
        }

        protected virtual void OnAfterItemUpdated(ItemInstance item)
        {
            foreach (Transform child in transform)
            {
                var slot = child.gameObject.GetComponent<MobileInventorySlotBehaviour>();
                if (slot.ItemId == item.Id)
                {
                    slot.RefreshSlot();
                }
            }
        }

        protected virtual void RefreshViewportHeight()
        {
            var gridLayoutGroup = GetComponent<GridLayoutGroup>();
            var contentRect = GetComponent<RectTransform>();
            contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, transform.childCount * (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y));
        }

        protected void DeleteItem(ItemInstance itemInstance)
        {
            Storage.Delete(itemInstance);
        }

    }
}