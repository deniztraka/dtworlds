using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Inventory.Models;
using UnityEngine;

namespace DTWorlds.Items.Inventory.Behaviours
{
    public abstract class BaseStorageBehaviour : MonoBehaviour
    {
        private BaseStorage baseStorage;
        public BaseStorage BaseStorage
        {
            get { return baseStorage; }
            set { baseStorage = value; }
        }

    }
}