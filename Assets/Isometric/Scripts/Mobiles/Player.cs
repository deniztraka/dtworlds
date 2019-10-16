using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Inventory.Models;
using Kryz.CharacterStats;
using UnityEngine;

namespace DTWorlds.Mobiles
{
    public class Player : Human
    {

        public Player(GameObject animationSprite, float movementSpeed) : base(animationSprite, movementSpeed)
        {
            Inventory = new MobileInventory(this);
        }
    }
}
