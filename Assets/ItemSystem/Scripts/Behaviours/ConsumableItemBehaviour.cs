using InventorySystem;
using DTWorlds.Items.Consumables;
using DTWorlds.Mobiles;

namespace DTWorlds.Items.Behaviours
{

    public class ConsumableItemBehaviour : ItemBehaviour
    {
        public void Use(BaseMobile mobile)
        {
            if (this.ItemInstance.Quantity > 0)
            {
                this.ItemInstance.Quantity--;

                if (this.ItemInstance.Quantity == 0)
                {
                    var inventoryItemBehaviour = this.gameObject.GetComponent<InventoryItemBehaviour>();
                    if (inventoryItemBehaviour != null)
                    {
                        //item is in inventory
                        var consumable = ItemInstance.ItemTemplate as BaseConsumable;
                        consumable.Use(mobile);
                    }
                }
            }
        }
    }
}