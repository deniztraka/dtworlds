using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Inventory.Models;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.Items.Inventory.Behaviours
{
    public abstract class BaseStorageSlotBehaviour : MonoBehaviour
    {
        private ItemInstance itemInstance;

        public Sprite SelectedImage;
        public Sprite NormalImage;

        public Color NormalColor;
        public Color SelectedColor;

        public Color WeakItemColor;
        public Color RegularItemColor;
        public Color ExceptionalItemColor;
        public Color RareItemColor;
        public Color LegendItemColor;

        public bool IsSelected;
        public bool HasItem;
        public string ItemId;

        public delegate void StorageSlotEventHandler(BaseStorageSlotBehaviour slot);
        public event StorageSlotEventHandler OnAfterSelected;

        private void Start()
        {
            GetComponent<Image>().color = NormalColor;
        }


        public virtual void AddItem(ItemInstance item)
        {
            ItemId = item.Id;
            HasItem = true;
            itemInstance = item;

            RefreshSlot();
        }

        public void OnClick()
        {
            if (HasItem && itemInstance != null)
            {
                SetSelected(!IsSelected);
                if (OnAfterSelected != null)
                {
                    OnAfterSelected(this);
                }
            }
        }

        public ItemInstance GetItemInstance()
        {
            return itemInstance;
        }

        public void SetSelected(bool newStatus)
        {
            IsSelected = newStatus;
            var image = GetComponent<Image>();
            

            image.color = IsSelected ? SelectedColor : NormalColor;
            image.sprite = IsSelected ? SelectedImage : NormalImage;
        }

        public virtual void RefreshSlot()
        {
            var titleTextComponent = transform.Find("ItemTitle").gameObject.GetComponent<Text>();
            var iconComponent = transform.Find("ItemIcon").gameObject.GetComponent<Image>();
            var descriptionTextComponent = transform.Find("ItemDescription").gameObject.GetComponent<Text>();
            var statsTextComponent = transform.Find("ItemStats").gameObject.GetComponent<Text>();
            var quantityTextComponent = transform.Find("ItemQuantity").gameObject.GetComponent<Text>();
            var qualityTextComponent = transform.Find("ItemQuality").gameObject.GetComponent<Text>();
            var equippedImageObjet = transform.Find("EquippedImage").gameObject;

            if (HasItem && itemInstance != null)
            {
                //Debug.Log(itemInstance.IsEquipped);
                equippedImageObjet.SetActive(itemInstance.IsEquipped);

                titleTextComponent.text = itemInstance.ItemTemplate.ItemName;
                iconComponent.sprite = itemInstance.ItemTemplate.Icon;
                descriptionTextComponent.text = itemInstance.ItemTemplate.ItemDescription;
                statsTextComponent.text = itemInstance.GetStatsText("{2}:<b>{1}{0}</b>  ");
                quantityTextComponent.text = itemInstance.Quantity > 1 ? "x" + itemInstance.Quantity.ToString() : String.Empty;
                qualityTextComponent.text = itemInstance.Quality.ToString();
                var itemColor = Color.black;
                switch (itemInstance.Quality)
                {
                    case ItemQuality.Weak:
                        itemColor = WeakItemColor;
                        break;
                    case ItemQuality.Regular:
                        itemColor = RegularItemColor;
                        break;
                    case ItemQuality.Exceptional:
                        itemColor = ExceptionalItemColor;
                        break;
                    case ItemQuality.Rare:
                        itemColor = RareItemColor;
                        break;
                    case ItemQuality.Legend:
                        itemColor = LegendItemColor;
                        break;
                }
                qualityTextComponent.color = itemColor;
            }
            else
            {
                titleTextComponent.text = String.Empty;
                iconComponent.sprite = null;
                descriptionTextComponent.text = String.Empty;
                statsTextComponent.text = String.Empty;
                quantityTextComponent.text = String.Empty;
                qualityTextComponent.text = String.Empty;
                qualityTextComponent.color = Color.black;
            }
        }
    }
}