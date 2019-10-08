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
        private Color tempColor;
        private InventoryItemBehaviour inventoryItemBehaviour;
        private InventoryBehaviour inventoryBehaviour;

        private InventorySlotBehaviour prevInventorySlotBehaviour;

        public Image ItemImage;
        public Text TitleText;
        public Text DescText;
        public Text StatsText;
        public VicinityPackBehaviour vicinityPackBehaviour;
        public Button DropButton;
        public Button EquipButton;
        public Button UnequipButton;
        public Button PickUpButton;
        public Button[] CharacterSlotSelectedButtons;
        public Button[] VicinitySlotSelectedButtons;
        public Button[] InventorySlotSelectedButtons;
        public Button[] StorageSlotSelectedButtons;
        public List<DragAndDropCell> CharacterSlotsList;
        public RectTransform CharacterStatsPanel;
        public RectTransform SelectedItemPanel;

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

                var tempInventorySlotBehaviour = msg.InventoryItemBehaviour.GetComponentInParent<InventorySlotBehaviour>();
                if (!tempInventorySlotBehaviour.Equals(prevInventorySlotBehaviour))
                {
                    if (prevInventorySlotBehaviour != null) { prevInventorySlotBehaviour.SetSelected(false); }
                    prevInventorySlotBehaviour = tempInventorySlotBehaviour;
                }

                SelectedItemPanel.gameObject.SetActive(true);
                CharacterStatsPanel.gameObject.SetActive(false);
                MakeAllButtonsDisabled();

                inventoryItemBehaviour = msg.InventoryItemBehaviour;

                ItemImage.sprite = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.Icon;
                tempColor.a = 1;
                ItemImage.color = tempColor;

                TitleText.text = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.ItemName;
                DescText.text = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.ItemDescription;
                StatsText.text = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.GetStatsText();

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

                var equipment = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate as BaseEquipment;
                if (equipment == null)
                {
                    if (EquipButton.interactable)
                    {
                        EquipButton.gameObject.SetActive(true);
                        EquipButton.interactable = false;
                    }
                }
            }
            else
            {
                CharacterStatsPanel.gameObject.SetActive(true);
                SelectedItemPanel.gameObject.SetActive(false);
            }
        }

        void OnInventoryItemUnSelected()
        {
            CharacterStatsPanel.gameObject.SetActive(true);
            SelectedItemPanel.gameObject.SetActive(false);
            inventoryItemBehaviour = null;

            ItemImage.sprite = null;
            tempColor.a = 0;
            ItemImage.color = tempColor;

            TitleText.text = String.Empty;
            DescText.text = String.Empty;
            StatsText.text = String.Empty;
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
                var selectedInventorySlot = inventoryItemBehaviour.GetComponentInParent<InventorySlotBehaviour>();

                var playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
                var equippableItem = inventoryItemBehaviour.ItemInstance.ItemTemplate as BaseEquipment;

                //find right slot to equip
                var dragAndDropCell = CharacterSlotsList.Find(s => s.equipmentType == equippableItem.EquipmentType).GetComponent<DragAndDropCell>();
                if (dragAndDropCell != null)
                {
                    var characterSlot = dragAndDropCell.GetComponent<CharacterSlotBehaviour>();

                    //if has item, uneqip it first
                    if (characterSlot.HasItem)
                    {
                        characterSlot.Unequip();
                        //Select again after unequip
                        inventoryItemBehaviour = selectedInventorySlot.GetInventoryItem();
                    }

                    //equip it
                    playerBehaviour.Equip(characterSlot, inventoryItemBehaviour);
                    var selectedSlot = inventoryBehaviour.GetSelectedSlot();
                    selectedSlot.DeleteItem();
                }
            }
        }

        public void OnUnEquipButtonClicked()
        {
            if (inventoryItemBehaviour != null)
            {
                 var playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
                 var chosenSlot = inventoryItemBehaviour.GetComponentInParent<CharacterSlotBehaviour>();
                 playerBehaviour.Unequip(chosenSlot);
                
               
            }
        }
    }
}

