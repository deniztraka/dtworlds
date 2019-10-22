﻿using System.Collections;
using System.Collections.Generic;
using DTWorlds.Items.Inventory.Behaviours.Vicinity;
using UnityEngine;

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
            }
            else
            {
                VicinityPanelBehaviour.transform.localScale = new Vector3(1, 1, 1);
                VicinityPanelBehaviour.CheckVicinity();  
            }

            isOpened = !isOpened;
        }
    }
}