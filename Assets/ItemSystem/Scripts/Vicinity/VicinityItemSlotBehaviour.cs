using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.Items.Inventory.Behaviours.Vicinity
{
    public class VicinityItemSlotBehaviour : MonoBehaviour
    {
        private ItemInstance itemInstance;
        private GameObject itemObject;
        public Image ItemImage;

        public void Pickup()
        {
            SendMessageUpwards("PickupVicinityItem", this.itemObject, SendMessageOptions.DontRequireReceiver);
            GameObject.Destroy(this.gameObject);
        }

        public void SetItem(ItemInstance itemInstance, Items.Behaviours.ItemBehaviour itemBehaviour)
        {
            this.itemObject = itemBehaviour.gameObject;
            this.itemInstance = itemInstance;
            ItemImage.sprite = itemInstance.ItemTemplate.Icon;
        }

    }
}