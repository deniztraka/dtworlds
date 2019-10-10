using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.Items.Equipments
{
    [CreateAssetMenu(fileName = "ShortPants.asset", menuName = "Items/Equipments/ShortPants", order = 1)]
    public class ShortPants : BaseEquipment
    {
        public override List<ItemBonus> GetBonusList()
        {
            return new List<ItemBonus>() { ArmorBonus, DexterityBonus };
        }
    }
}
