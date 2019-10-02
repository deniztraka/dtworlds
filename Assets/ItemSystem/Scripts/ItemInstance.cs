using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.Items
{

    [Serializable]
    public class ItemInstance
    {
        public BaseItem ItemTemplate;
        public int Quantity;

        public T getCopy<T>()
        {
            return (T)this.MemberwiseClone();
        }
    }
}