using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
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
            }
        }

        protected override void OnAfterItemDeleted(ItemInstance item)
        {
            var createdGameObject = Instantiate(item.ItemTemplate.ItemPrefab, BaseMobileBehaviour.transform.position, Quaternion.identity);
            var itemBehaviour = createdGameObject.GetComponent<ItemBehaviour>();
            itemBehaviour.ItemInstance = item;

            var gridLayoutGroup = GetComponent<GridLayoutGroup>();
            var contentRect = GetComponent<RectTransform>();
            contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentRect.rect.height - gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);
        }

    }
}