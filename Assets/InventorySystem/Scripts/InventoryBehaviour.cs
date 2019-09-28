using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class InventoryBehaviour : MonoBehaviour
    {
        public GameObject InventorySlotPrefab;
        public GameObject ExampleInventoryItemPrefab;
        private int sizeX = 5;
        private int sizeY = 7;
        private bool isInitialized;
        private GameObject[][] SlotGrid;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
            if (ExampleInventoryItemPrefab != null)
            {
                PlaceExampleItem();
            }
        }

        void Initialize()
        {
            if (isInitialized)
            {
                return;
            }

            SlotGrid = new GameObject[sizeX][];

            for (int x = 0; x < SlotGrid.Length; x++)
            {
                if (SlotGrid[x] == null)
                {
                    SlotGrid[x] = new GameObject[sizeY];
                }
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    SlotGrid[x][y] = Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
                }
            }
            isInitialized = true;
        }

        public void AddItem(DragAndDropItem item)
        {
            var emptySlot = GetEmptySlot();
            if (emptySlot == null)
            {
                return;
            }

            emptySlot.AddItem(item);
        }

        public InventorySlotBehaviour GetEmptySlot()
        {
            if (!isInitialized)
            {
                return null;
            }

            InventorySlotBehaviour slotToBeReturned = null;

            for (int x = 0; x < SlotGrid.Length; x++)
            {
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    if (SlotGrid[x][y] != null)
                    {
                        var inventoryItem = SlotGrid[x][y].GetComponentInChildren<DragAndDropItem>();
                        if (inventoryItem == null)
                        {
                            slotToBeReturned = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>();
                            break;
                        }
                    }
                }

                if (slotToBeReturned != null)
                {
                    break;
                }
            }

            return slotToBeReturned;
        }

        void PlaceExampleItem()
        {
            var slotBehaviour = SlotGrid[0][0].GetComponent<InventorySlotBehaviour>();
            var dragAndDropItemObj = Instantiate(ExampleInventoryItemPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            slotBehaviour.AddItem(dragAndDropItemObj.GetComponent<DragAndDropItem>());
        }
    }
}