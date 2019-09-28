using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem.Interfaces;

namespace InventorySystem.UI
{
    public class SelectedItemPanelBehaviour : MonoBehaviour
    {
        public Image ItemImage;
        private Color tempColor;

        void Start()
        {
            ItemImage.sprite = null;            
            tempColor = ItemImage.color;
            tempColor.a = 0;
            ItemImage.color = tempColor;
        }

        void OnInventoryItemSelected(IInventoryItem inventoryItem)
        {
            if (inventoryItem != null)
            {
                ItemImage.sprite = inventoryItem.Icon.sprite;
                tempColor.a = 1;
                ItemImage.color = tempColor;
            }
        }

        void OnInventoryItemUnSelected()
        {
            ItemImage.sprite = null;
            tempColor.a = 0;
            ItemImage.color = tempColor;
        }
    }
}