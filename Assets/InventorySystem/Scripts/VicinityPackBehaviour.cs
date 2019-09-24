using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace InventorySystem
{
    public class VicinityPackBehaviour : InventoryBehaviour
    {
        public GameObject PlayerObject;
        private Collider2D[] results = new Collider2D[35];

        // Update is called once per frame
        void Update()
        {

        }

        public void CheckVicinity()
        {
            if (PlayerObject != null)
            {
                
                var radius = PlayerObject.GetComponent<CircleCollider2D>().radius;
                var resultCount = Physics2D.OverlapCircleNonAlloc(PlayerObject.transform.position, radius, results,LayerMask.GetMask("Floor"));
                if(resultCount > 0){
                    for (int i = 0; i < resultCount; i++)
                    {
                        
                    }
                    //Debug.Log("Items found");
                }else {
                    //Debug.Log("Nothing in vicinity");
                }
            }

        }
    }
}