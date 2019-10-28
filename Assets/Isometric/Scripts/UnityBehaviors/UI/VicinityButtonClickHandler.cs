using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Inventory.Behaviours.Vicinity;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.UnityBehaviours.UI
{
    public class VicinityButtonClickHandler : MonoBehaviour
    {
        public VicinityPanelBehaviour VicinityPanelBehaviour;
        private bool isOpened;

        public void ToggleVicinityPanel()
        {

            if (isOpened)
            {
                VicinityPanelBehaviour.transform.localScale = new Vector3(0, 0, 0);                                  
                VicinityPanelBehaviour.Clear();
                VicinityPanelBehaviour.IsOpen = false;
                
            }
            else
            {
                VicinityPanelBehaviour.transform.localScale = new Vector3(1, 1, 1);                
                VicinityPanelBehaviour.IsOpen = true;
                VicinityPanelBehaviour.CheckVicinity();
                VicinityPanelBehaviour.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1f; 
            }

            isOpened = !isOpened;
        }
    }
}