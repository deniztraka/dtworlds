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
        public Button PickUpButton;

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

        void OnInventoryItemSelected(SelectedItemMessage msg)
        {
            if (msg.InventoryItemBehaviour != null)
            {
                inventoryItemBehaviour = msg.InventoryItemBehaviour;

                ItemImage.sprite = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.Icon;
                tempColor.a = 1;
                ItemImage.color = tempColor;

                TitleText.text = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.ItemName;
                DescText.text = msg.InventoryItemBehaviour.ItemInstance.ItemTemplate.ItemDescription;

                DropButton.interactable = msg.IsInPlayerInventory;
                PickUpButton.gameObject.SetActive(true);
                PickUpButton.interactable = !msg.IsInPlayerInventory && inventoryItemBehaviour.GetComponentInParent<VicinityPackBehaviour>().GetEmptySlot() != null;

                EquipButton.gameObject.SetActive(false);
                EquipButton.interactable = false;

                if (inventoryItemBehaviour.ItemInstance.ItemTemplate is BaseEquipment && msg.IsInPlayerInventory)
                {
                    EquipButton.gameObject.SetActive(true);
                    EquipButton.interactable = true;
                    PickUpButton.gameObject.SetActive(false);
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

            DropButton.interactable = false;
            EquipButton.interactable = false;
            PickUpButton.interactable = false;
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
            // 
        }

        public void OnDropButtonClicked()
        {
            if (inventoryItemBehaviour != null)
            {
                inventoryBehaviour.DropSelectedItem();
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
                    var chosenSlot = dragAndDropCell.GetComponent<InventorySlotBehaviour>();

                    //if has item, uneqip it first
                    // if (chosenSlot.HasItem)
                    // {
                    //     playerBehaviour.Unequip(chosenSlot.GetInventoryItem());
                    // }
                    
                    //equip it
                    playerBehaviour.Equip(chosenSlot, inventoryItemBehaviour);
                }
            }
        }
    }
}