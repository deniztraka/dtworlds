using System;
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items;
using DTWorlds.Items.Behaviours;
using DTWorlds.UnityBehaviours;
using UnityEngine;
namespace InventorySystem
{
    public class VicinityPackBehaviour : InventoryBehaviour
    {
        Dictionary<string, GameObject> relations = new Dictionary<string, GameObject>();

        public GameObject PlayerObject;
        private InventoryBehaviour playerInventory;
        private Collider2D[] results = new Collider2D[35];

        public override void Start()
        {
            base.Start();
            playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().InventoryBehaviour;
        }

        public void CheckVicinity()
        {
            if (PlayerObject != null)
            {

                var radius = PlayerObject.GetComponent<CircleCollider2D>().radius;
                var resultCount = Physics2D.OverlapCircleNonAlloc(PlayerObject.transform.position, radius, results, LayerMask.GetMask("Floor"));
                if (resultCount > 0)
                {
                    //add found items to vicinity pack
                    for (int i = 0; i < resultCount; i++)
                    {
                        var itemBehaviour = results[i].GetComponent<ItemBehaviour>();
                        //check already pickedupItems
                        var foundSame = false;
                        foreach (var entry in relations)
                        {
                            if (entry.Value.Equals(results[i].gameObject))
                            {
                                foundSame = true;                                
                            }
                            break;
                        }
                        if (!foundSame)
                        {                           
                            // make relations in case of pickups
                            var slot = AddItem(itemBehaviour);
                            relations.Add(slot.SlotIndex, results[i].gameObject);
                        }
                    }
                }
            }

        }

        internal void Clear()
        {
            for (int x = 0; x < SlotGrid.Length; x++)
            {
                for (int y = 0; y < SlotGrid[x].Length; y++)
                {
                    if (SlotGrid[x][y] != null)
                    {
                        var slotBehaviour = SlotGrid[x][y].GetComponent<InventorySlotBehaviour>();
                        if (slotBehaviour.GetComponentInChildren<InventoryItemBehaviour>() != null)
                        {
                            slotBehaviour.DeleteItem();
                        }
                    }
                }
            }

            //Clearing the references
            relations = new Dictionary<string, GameObject>();
        }

        internal void DeleteRelatedItem(string slotIndex)
        {
            Destroy(relations[slotIndex].GetComponent<ItemBehaviour>().gameObject);
            relations.Remove(slotIndex);
        }

        internal ItemBehaviour GetSelectedRelatedItem()
        {
            var selectedSlot = GetSelectedSlot();
            if (selectedSlot != null)
            {
                return relations[selectedSlot.SlotIndex].GetComponent<ItemBehaviour>();
            }

            return null;
        }

        internal void Pickup()
        {
            var actualItem = GetSelectedRelatedItem();
            if (actualItem != null)
            {
                this.playerInventory.AddItem(actualItem);
            }

            var selectedSlot = GetSelectedSlot();
            selectedSlot.DeleteItem();
            relations.Remove(selectedSlot.SlotIndex);
            Destroy(actualItem.gameObject);
            Debug.Log(relations);
        }

        internal void AddItemRelation(string slotIndex, GameObject gameObject)
        {
            relations.Add(slotIndex, gameObject);
        }
    }
}