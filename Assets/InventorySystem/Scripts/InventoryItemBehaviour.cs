using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{

    public class InventoryItemBehaviour : MonoBehaviour
    {
        [SerializeField]
        private ItemInstance itemInstance;
        public ItemInstance ItemInstance
        {
            get { return itemInstance; }
            set
            {
                itemInstance = value;
                SetItem();
            }
        }

        internal void SetItem()
        {
            var itemImage = GetComponent<Image>();
            if (ItemInstance != null)
            {

                itemImage.sprite = itemInstance.ItemTemplate.Icon;

                var rectTransform = gameObject.GetComponent<RectTransform>();
                rectTransform.anchoredPosition3D = new Vector3(0f, 0f, 0f);
                rectTransform.anchoredPosition = new Vector2(0f, 0f);
            }
            else
            {
                itemImage.sprite = null;
                Destroy(gameObject);
            }

        }
    }
}