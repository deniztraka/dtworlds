using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
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

        public virtual void AddItem(ItemInstance item)
        {
            Storage.AddItem(item);
        }

        public virtual void Use()
        {

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

        protected virtual void OnAfterItemAdded(ItemInstance item)
        {
            var instantiatedSlot = Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, this.transform) as BaseStorageSlotBehaviour;
            instantiatedSlot.AddItem(item);
            AttachSlotEvents(instantiatedSlot);

            var gridLayoutGroup = GetComponent<GridLayoutGroup>();
            var contentRect = GetComponent<RectTransform>();
            contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentRect.rect.height + gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);
        }

        protected virtual void OnAfterItemDeleted(ItemInstance item)
        {
            var createdGameObject = Instantiate(item.ItemTemplate.ItemPrefab, transform.position, Quaternion.identity);
            var itemBehaviour = createdGameObject.GetComponent<ItemBehaviour>();
            itemBehaviour.ItemInstance = item;

            var gridLayoutGroup = GetComponent<GridLayoutGroup>();
            var contentRect = GetComponent<RectTransform>();
            contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentRect.rect.height - gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);
        }

    }
}