using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{

    public class InventorySlotBehaviour : MonoBehaviour
    {
        private DragAndDropCell dragAndDropCell;

        // Start is called before the first frame update
        void Awake()
        {
            dragAndDropCell = GetComponent<DragAndDropCell>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddItem(DragAndDropItem item){
            dragAndDropCell.AddItem(item);
        }
    }
}