using System.Collections;
using System.Collections.Generic;
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

        protected virtual void Start()
        {


        }

        protected virtual void OnAfterItemAdded(ItemInstance item)
        {
            var instantiatedSlot = Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, this.transform) as BaseStorageSlotBehaviour;
            instantiatedSlot.AddItem(item);

            var gridLayoutGroup = GetComponent<GridLayoutGroup>();
            var contentRect = GetComponent<RectTransform>();
            contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentRect.rect.height + gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);
        }

        public virtual void AddItem(ItemInstance item)
        {
            Storage.AddItem(item);
        }

    }
}