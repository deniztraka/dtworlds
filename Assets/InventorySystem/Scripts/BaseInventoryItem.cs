using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem.Interfaces;
using UnityEngine.UI;

namespace InventorySystem
{
    public abstract class BaseInventoryItem : IInventoryItem
    {
        private Image icon;
        public Image Icon
        {
            get { return icon; }
            set { icon = value; }
        }
        
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
    }
}