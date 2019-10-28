using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DTWorlds.Items.Behaviours;
using DTWorlds.Mobiles;
using DTWorlds.UnityBehaviours;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.Items.Inventory.Behaviours.Vicinity
{
    public class VicinityPanelBehaviour : MonoBehaviour
    {
        private IEnumerator coroutine;
        private GameObject playerObject;
        private Collider2D[] results = new Collider2D[999];
        private List<ItemInstance> ItemList;

        public GameObject VicinityItemSlotPrefab;
        public GameObject SlotContentObject;

        public Player Player;

        public bool IsOpen;



        void Start()
        {

            ItemList = new List<ItemInstance>();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            var playerBehaviour = playerObject.GetComponent<PlayerBehaviour>();
            Player = playerBehaviour.Mobile as Player;


            StartCoroutine(StartCheck(Player));
        }

        IEnumerator StartCheck(Player player)
        {
            //Debug.Log(IsOpen);
            while (true)
            {
                if (IsOpen && player.IsMoving)
                {
                    //Debug.Log("Checking vicinity items");
                    CheckVicinity();
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        internal void Clear()
        {
            ItemList = new List<ItemInstance>();
            foreach (Transform child in SlotContentObject.transform)
            {
                GameObject.DestroyImmediate(child.gameObject);
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
            Clear();
            if (playerObject != null && IsOpen)
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

            RefreshViewportHeight();
        }

        protected virtual void RefreshViewportHeight()
        {
            var gridLayoutGroup = SlotContentObject.GetComponent<GridLayoutGroup>();
            var contentRect = SlotContentObject.GetComponent<RectTransform>();
            var height = gridLayoutGroup.transform.childCount * (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y);
            contentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }
    }
}