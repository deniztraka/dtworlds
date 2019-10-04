using DTWorlds.Items.Behaviours;
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
        private Color tempColor;
        private InventoryItemBehaviour inventoryItemBehaviour;
        private InventoryBehaviour inventoryBehaviour;
        public Button DropButton;
        public Button EquipButton;
        public Button PickUpButton;

        void Start()
        {
            ItemImage.sprite = null;
            tempColor = ItemImage.color;
            tempColor.a = 0;
            ItemImage.color = tempColor;

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

                DropButton.interactable = msg.IsInPlayerInventory;
                PickUpButton.interactable = !msg.IsInPlayerInventory && inventoryItemBehaviour.GetComponentInParent<VicinityPackBehaviour>().GetEmptySlot() != null;
            }
        }

        void OnInventoryItemUnSelected()
        {
            inventoryItemBehaviour = null;

            ItemImage.sprite = null;
            tempColor.a = 0;
            ItemImage.color = tempColor;

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
            }
            // 
        }
    }
}