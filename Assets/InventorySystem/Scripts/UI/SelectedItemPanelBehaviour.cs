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

        void Start() {
            ItemImage.sprite = null;
        }

        void OnInventoryItemSelected(IInventoryItem inventoryItem)
        {
            if(inventoryItem != null){
                ItemImage.sprite = inventoryItem.Icon.sprite;
            }
        }

        void OnInventoryItemUnSelected()
        {
            ItemImage.sprite = null;
        }
    }
}