using InventorySystem;
using DTWorlds.Items.Consumables;
using DTWorlds.Mobiles;
using System;

namespace DTWorlds.Items.Behaviours
{

    public class ConsumableItemBehaviour : ItemBehaviour
    {
        internal void Consume(BaseMobile mobile)
        {
            if (this.ItemInstance.Quantity > 0)
            {

                //item is in inventory
                var consumable = ItemInstance.ItemTemplate as BaseConsumable;
                consumable.Consume(ItemInstance, mobile);

                this.ItemInstance.Quantity--;

                if (this.ItemInstance.Quantity <= 0)
                {
                    if (this.ItemInstance.Quantity <= 0)
                    {
                        var inventorySlot = this.GetComponentInParent<InventorySlotBehaviour>();
                        inventorySlot.DeleteItem();

                    }

                }

            }
        }
    }
}