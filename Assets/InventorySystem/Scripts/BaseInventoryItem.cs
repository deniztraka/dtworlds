using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem.Interfaces;
using UnityEngine.UI;
using DTWorlds.Items;

namespace InventorySystem
{
    public abstract class BaseInventoryItem : MonoBehaviour
    {
        public BaseItem Item;

        public GameObject ItemQuantityPanel;
        public GameObject ItemTexturePanel;
        
    }
}