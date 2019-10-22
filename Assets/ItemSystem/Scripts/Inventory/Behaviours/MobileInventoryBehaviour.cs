using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
using DTWorlds.Items.Consumables;
using DTWorlds.Items.Inventory.Models;
using DTWorlds.UnityBehaviours;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.Items.Inventory.Behaviours
{
    public class MobileInventoryBehaviour : BaseStorageBehaviour
    {

        public BaseMobileBehaviour BaseMobileBehaviour;

        protected override void Start()
        {
            base.Start();

            if (Storage == null && BaseMobileBehaviour != null)
            {
                Storage = BaseMobileBehaviour.Mobile.Inventory;
                Storage.OnAfterItemAdded += new BaseStorage.StorageEventHandler(OnAfterItemAdded);
                Storage.OnAfterItemDeleted += new BaseStorage.StorageEventHandler(OnAfterItemDeleted);
                Storage.OnAfterItemUpdated += new BaseStorage.StorageEventHandler(OnAfterItemUpdated);

                Storage.MaxWeight = BaseMobileBehaviour.Mobile.Strength.Value * 10;
                BaseMobileBehaviour.Mobile.Strength.OnAfterValueChangedEvent += new Kryz.CharacterStats.CharacterStat.CharacterStatEventHandler(UpdateMaxWeight);
            }
        }

        protected void UpdateMaxWeight()
        {
            Storage.MaxWeight = BaseMobileBehaviour.Mobile.Strength.Value * 10;
        }

        protected override void OnAfterItemDeleted(ItemInstance item)
        {
            if (item.Quantity <= 0)
            {
                var selectedSlot = GetSelectedSlot();
                selectedSlot.transform.SetParent(null);
                Destroy(selectedSlot.gameObject);
            }
            else
            {
                var createdGameObject = Instantiate(item.ItemTemplate.ItemPrefab, BaseMobileBehaviour.transform.position, Quaternion.identity);
                var itemBehaviour = createdGameObject.GetComponent<ItemBehaviour>();
                itemBehaviour.ItemInstance = item;
            }
            RefreshViewportHeight();
        }

        public override void Use()
        {
            var selectedSlot = GetSelectedSlot();
            if (selectedSlot != null)
            {
                var itemInstance = selectedSlot.GetItemInstance();
                if (itemInstance.IsUsuable())
                {
                    var consumable = itemInstance.ItemTemplate as BaseConsumable;
                    var consumeResult = consumable.Consume(itemInstance, BaseMobileBehaviour.Mobile);
                    if (consumeResult)
                    {

                        itemInstance.Quantity--;
                        UpdateItem(itemInstance);

                        if (itemInstance.Quantity <= 0)
                        {
                            DeleteItem(itemInstance);
                        }
                    }
                }
            }
        }

        public override void Drop()
        {
            var selectedSlot = GetSelectedSlot();
            if (selectedSlot != null)
            {
                var itemInstance = selectedSlot.GetItemInstance();
                if (itemInstance.IsEquipped)
                {
                    UnEquip(itemInstance);
                }

                Storage.Delete(itemInstance);
                Destroy(selectedSlot.gameObject);

                SelectedItemPanelBehaviour.SetPanel(null);
                SelectedItemPanelBehaviour.gameObject.SetActive(false);
            }
        }

        public virtual void Equip(ItemInstance itemInstance)
        {

            //Check if any equipped item in same slot, then unequip it.
            var equipedItem = Storage.GetSameTypeEquippedItem(itemInstance.ItemTemplate.Type);
            if (equipedItem != null)
            {
                UnEquip(equipedItem);
            }

            //if its get this far, we have item here
            var playerBehaviour = BaseMobileBehaviour as PlayerBehaviour;
            playerBehaviour.Equip(itemInstance);
            UpdateItem(itemInstance);
            RenderProperButtons();

            SelectedItemPanelBehaviour.SetPanel(itemInstance);
        }

        public virtual void UnEquip(ItemInstance equipedItem)
        {
            if (equipedItem != null)
            {
                var playerBehaviour = BaseMobileBehaviour as PlayerBehaviour;
                playerBehaviour.Unequip(equipedItem);
                UpdateItem(equipedItem);
                RenderProperButtons();
                SelectedItemPanelBehaviour.SetPanel(equipedItem);
            }
        }
    }
}