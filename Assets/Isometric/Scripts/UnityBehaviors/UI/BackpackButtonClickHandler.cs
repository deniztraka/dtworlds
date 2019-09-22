using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;

namespace DTWorlds.UnityBehaviours.UI
{
    public class BackpackButtonClickHandler : MonoBehaviour
    {
        public GameObject InventoryPanel;
        public VicinityPackBehaviour VicinityPackBehaviour;
        private bool isOpened;

        public void ToggleInventoryPanel()
        {

            if (isOpened)
            {
                InventoryPanel.transform.localScale = new Vector3(0, 0, 0);     
                           
            }
            else
            {
                InventoryPanel.transform.localScale = new Vector3(1, 1, 1);
                VicinityPackBehaviour.CheckVicinity();
            }

            isOpened = !isOpened;
        }
    }
}