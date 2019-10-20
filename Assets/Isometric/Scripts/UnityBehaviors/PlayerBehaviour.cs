using System.Collections;
using System.Collections.Generic;
using DTWorlds.Mobiles;
using DTWorlds.Mobiles.MovementTypes;
using DTWorlds.Mobiles.MovementInputs;
using UnityEngine;
using InventorySystem;
using DTWorlds.Items.Equipments;
using System;
using InventorySystem.UI;
using DTWorlds.Items;
using DTWorlds.Items.Inventory.CharacterEquipments.Behaviours;

namespace DTWorlds.UnityBehaviours
{
    public class PlayerBehaviour : BaseMobileBehaviour
    {
        public CharacterEquipmentPanelBehaviour CharacterEquipmentsPanelBehaviour;

        private void buildPlayer()
        {
            var player = new Player(gameObject, 1);
            player.SetMovementType(new IsometricMovement(new JoyStickMovementInput(joystick)));
            player.Health.CurrentValue = 50;
            //player.SetMovementType(new IsometricMovement(new KeyboardMovementInput()));
            InitMobile(player);
        }

        void Awake()
        {
            buildPlayer();
        }

        public void Equip(CharacterSlotBehaviour chosenSlot, InventoryItemBehaviour inventoryItem)
        {
            if (inventoryItem.ItemInstance.ItemTemplate is BaseEquipment)
            {
                chosenSlot.AddItem(inventoryItem.ItemInstance);
            }
        }

        internal void Unequip(CharacterSlotBehaviour chosenSlot)
        {
            chosenSlot.Unequip();
        }

        internal void Unequip(ItemInstance selectedItem)
        {
            selectedItem.SetUnEquipped(Mobile);
            CharacterEquipmentsPanelBehaviour.SetSlot(selectedItem);
        }

        internal void Equip(ItemInstance selectedItem)
        {
            selectedItem.SetEquipped(Mobile);
            CharacterEquipmentsPanelBehaviour.SetSlot(selectedItem);
        }
    }
}