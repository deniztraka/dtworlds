using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DTWorlds.Items.Behaviours;
using DTWorlds.Mobiles;
using DTWorlds.UnityBehaviours;
using UnityEngine;
namespace DTWorlds.Items.Inventory.Behaviours.Vicinity
{
    public class VicinityPanelBehaviour : MonoBehaviour
    {
        private GameObject playerObject;
        private Collider2D[] results = new Collider2D[999];
        private List<ItemInstance> ItemList;

        public GameObject VicinityItemSlotPrefab;
        public GameObject SlotContentObject;

        public Player Player;



        void Start()
        {

            ItemList = new List<ItemInstance>();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            var playerBehaviour = playerObject.GetComponent<PlayerBehaviour>();
            Player = playerBehaviour.Mobile as Player;
        }

        internal void Clear()
        {
            ItemList = new List<ItemInstance>();
            foreach (Transform child in SlotContentObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

        public void PickupVicinityItem(GameObject gameObject)
        {
            var itemInstance = gameObject.GetComponent<ItemBehaviour>().ItemInstance;
            Player.Inventory.AddItem(itemInstance);
            ItemList.Remove(itemInstance);
            Destroy(gameObject);
        }

        public void CheckVicinity()
        {
            if (playerObject != null)
            {

                var radius = playerObject.GetComponent<CircleCollider2D>().radius;
                var resultCount = Physics2D.OverlapCircleNonAlloc(playerObject.transform.position, radius, results, LayerMask.GetMask("Floor"));
                if (resultCount > 0)
                {

                    for (int i = 0; i < resultCount; i++)
                    {
                        var itemBehaviour = results[i].GetComponent<ItemBehaviour>();
                        var itemInstance = itemBehaviour.ItemInstance;
                        var foundItem = ItemList.FirstOrDefault(j => j.Id == itemBehaviour.ItemInstance.Id);
                        if (foundItem == null)
                        {
                            var vicinityItemSlotBehaviour = Instantiate(VicinityItemSlotPrefab, Vector3.zero, Quaternion.identity, SlotContentObject.transform).GetComponent<VicinityItemSlotBehaviour>();
                            vicinityItemSlotBehaviour.SetItem(itemInstance, itemBehaviour);
                            ItemList.Add(itemInstance);
                        }
                    }
                }
            }
        }
    }
}