using System;
using System.Collections.Generic;
using DTWorlds.Items.Behaviours;
using DTWorlds.Items.Equipments;
using DTWorlds.UnityBehaviours;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.UI
{
    public class SelectedItemMessage
    {
        public InventoryItemBehaviour InventoryItemBehaviour;
        public bool IsInPlayerInventory;

        public SelectedItemMessage(InventoryItemBehaviour inventoryItemBehaviour, bool isInPlayerInventory)
        {
            InventoryItemBehaviour = inventoryItemBehaviour;
            IsInPlayerInventory = isInPlayerInventory;
        }
    }

    public class SelectedItemPanelBehaviour : MonoBehaviour
    {
        public Image ItemImage;
        public Text TitleText;
        public Text DescText;
        public Text StatsText;
        private Color tempColor;
        private InventoryItemBehaviour inventoryItemBehaviour;
        public VicinityPackBehaviour vicinityPackBehaviour;
        private InventoryBehaviour inventoryBehaviour;
        public Button DropButton;
        public Button EquipButton;
        public Button UnequipButton;
        public Button PickUpButton;

        public Button[] CharacterSlotSelectedButtons;
        public Button[] VicinitySlotSelectedButtons;
        public Button[] InventorySlotSelectedButtons;
        public Button[] StorageSlotSelectedButtons;

        public List<DragAndDropCell> CharacterSlotsList;

        void Start()
        {
            ItemImage.sprite = null;
            tempColor = ItemImage.color;
            tempColor.a = 0;
            ItemImage.color = tempColor;
            TitleText.text = String.Empty;
            DescText.text = String.Empty;
            StatsText.text = String.Empty;

            inventoryBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().InventoryBehaviour;
        }

        void MakeAllButtonsDisabled()
        {
            SetButtonsStatus(new Button[] { DropButton, EquipButton, UnequipButton, PickUpButton }, false);
        }

        void SetButtonsStatus(Button[] buttonList, bool status)
        {
            foreach (var button in buttonList)
            {
                button.gameObject.SetActive(status);
                button.interactable = status;
            }
        }

        void OnInventoryItemSelected(SelectedItemMessage msg)
        {
            if (msg.InventoryItemBehaviour != null)
            {
                MakeAllButtonsDisabled();

                inventoryItemBehaviour = msg.InventoryItemBehaviour;

                ItemImage.sprite = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.Icon;
                tempColor.a = 1;
                ItemImage.color = tempColor;

                TitleText.text = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.ItemName;
                DescText.text = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.ItemDescription;

                var vicinityPackBehaviour = inventoryItemBehaviour.GetComponentInParent<VicinityPackBehaviour>();

                if (msg.IsInPlayerInventory)
                {
                    SetButtonsStatus(InventorySlotSelectedButtons, true);
                }
                else if (vicinityPackBehaviour != null)
                {
                    SetButtonsStatus(VicinitySlotSelectedButtons, true);
                }
                else if (inventoryItemBehaviour.GetComponentInParent<CharacterSlotBehaviour>() != null)
                {
                    SetButtonsStatus(CharacterSlotSelectedButtons, true);
                }
            }
        }

        void OnInventoryItemUnSelected()
        {
            inventoryItemBehaviour = null;

            ItemImage.sprite = null;
            tempColor.a = 0;
            ItemImage.color = tempColor;

            TitleText.text = String.Empty;
            DescText.text = String.Empty;

            MakeAllButtonsDisabled();
        }

        public void OnPickupButtonClicked()
        {
            if (inventoryItemBehaviour != null)
            {
                switch (inventoryItemBehaviour.GetComponentInParent<InventoryBehaviour>().GetType().Name)
                {
                    case "VicinityPackBehaviour":
                        var vicinityPackBehaviour = inventoryItemBehaviour.GetComponentInParent<VicinityPackBehaviour>();
                        vicinityPackBehaviour.Pickup();
                        break;
                    case "InventoryBehaviour":
                        var inventoryBehaviour = inventoryItemBehaviour.GetComponentInParent<InventoryBehaviour>();
                        break;

                        //default:
                }
            }
        }

        public void OnDropButtonClicked()
        {
            if (inventoryItemBehaviour != null)
            {

                var characterSlot = inventoryItemBehaviour.GetComponentInParent<CharacterSlotBehaviour>();
                if (characterSlot != null)
                {
                    //this is already equipped item
                    characterSlot.DropItem();
                }
                else// it is in inventory
                {
                    inventoryBehaviour.DropSelectedItem();
                }

                OnInventoryItemUnSelected();

                vicinityPackBehaviour.CheckVicinity();
            }
        }

        public void OnEquipButtonClicked()
        {
            if (inventoryItemBehaviour != null)
            {
                var playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
                var equippableItem = inventoryItemBehaviour.ItemInstance.ItemTemplate as BaseEquipment;

                //find right slot to equip
                var dragAndDropCell = CharacterSlotsList.Find(s => s.equipmentType == equippableItem.EquipmentType).GetComponent<DragAndDropCell>();
                if (dragAndDropCell != null)
                {
                    var chosenSlot = dragAndDropCell.GetComponent<CharacterSlotBehaviour>();

                    //TODO: UNEQUIP FIFRST
                    //if has item, uneqip it first
                    // if (chosenSlot.HasItem)
                    // {
                    //     playerBehaviour.Unequip(chosenSlot.GetInventoryItem());
                    //     characterSlotBehaviour.UpdateImage();
                    // }

                    //equip it
                    playerBehaviour.Equip(chosenSlot, inventoryItemBehaviour);
                    var selectedSlot = inventoryBehaviour.GetSelectedSlot();
                    selectedSlot.DeleteItem();
                }
            }
        }

        public void OnUnEquipButtonClicked()
        {
            if (inventoryItemBehaviour != null)
            {

            }
        }
    }
}

