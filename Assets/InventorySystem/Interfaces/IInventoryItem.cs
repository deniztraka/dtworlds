using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace InventorySystem.Interfaces
{
    public interface IInventoryItem
    {
        Image Icon { get; set; }
        string Name { get; set; }
    }
}