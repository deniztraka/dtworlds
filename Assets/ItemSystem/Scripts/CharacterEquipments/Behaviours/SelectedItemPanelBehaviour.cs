using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
using DTWorlds.Items.Consumables;
using DTWorlds.Items.Equipments;
using DTWorlds.Items.Inventory.Behaviours;
using DTWorlds.Items.Inventory.Models;
using DTWorlds.UnityBehaviours;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.Items.Inventory.Behaviours
{
    public class SelectedItemPanelBehaviour : MonoBehaviour
    {
        private ItemInstance itemInstance;

        public PlayerInventoryBehaviour PlayerInventoryBehaviour;
        private Color tempColor;
        public Image ItemImage;
        public Text TitleText;
        public Text QualityText;
        public Text DescText;
        public Text LongDescText;
        public Text StatsText;
        public Button UnequipButton;

        public Color WeakItemColor;
        public Color RegularItemColor;
        public Color ExceptionalItemColor;
        public Color RareItemColor;
        public Color LegendItemColor;

        void Start()
        {
            // ItemImage.sprite = null;
            // tempColor = ItemImage.color;
            // tempColor.a = 0;
            // ItemImage.color = tempColor;
            // TitleText.text = String.Empty;
            // DescText.text = String.Empty;
            // StatsText.text = String.Empty;
            // QualityText.text = String.Empty;
        }

        internal void SetPanel(ItemInstance itemInstance)
        {
            if (itemInstance != null)
            {
                this.itemInstance = itemInstance;
                ItemImage.sprite = itemInstance.ItemTemplate.Icon;
                //ItemImage.color = tempColor;

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

                TitleText.color = itemColor;
                QualityText.color = itemColor;

                TitleText.text = itemInstance.ItemTemplate.ItemName;
                DescText.text = itemInstance.ItemTemplate.ItemDescription;
                LongDescText.text = itemInstance.ItemTemplate.ItemLongDescription;
                QualityText.text = itemInstance.GetQualityText();
                StatsText.text = itemInstance.GetStatsText(" {2}:<b>{1}{0}</b> ");



                UnequipButton.gameObject.SetActive(itemInstance.IsEquippable());
                UnequipButton.interactable = itemInstance.IsEquipped;
            }
            else
            {
                UnequipButton.gameObject.SetActive(false);
                UnequipButton.interactable = false;
            }
        }

        public void UnEquip()
        {
            if (itemInstance != null)
            {
                PlayerInventoryBehaviour.UnEquip(itemInstance);
            }

        }
    }
}