using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.UnityBehaviours.UI
{
    public class BackpackButtonClickHandler : MonoBehaviour
    {
        public GameObject InventoryPanel;
        private bool isOpened;

        public void ToggleInventoryPanel()
        {

            if (isOpened)
            {
                InventoryPanel.transform.localScale = new Vector3(0, 0, 0);     
                //VicinityPackBehaviour.Clear();
            }
            else
            {
                InventoryPanel.transform.localScale = new Vector3(1, 1, 1);
                InventoryPanel.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1f; 
                //VicinityPackBehaviour.CheckVicinity();
            }

            isOpened = !isOpened;
        }
    }
}