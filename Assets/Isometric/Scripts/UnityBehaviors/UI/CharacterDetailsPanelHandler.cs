using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DTWorlds.UnityBehaviours.UI
{
    public class CharacterDetailsPanelHandler : MonoBehaviour
    {
        private bool isOpened;

        public void ToggleCharacterDetailsPanel()
        {

            if (isOpened)
            {
                gameObject.transform.localScale = new Vector3(0, 0, 0);
                //VicinityPackBehaviour.Clear();
            }
            else
            {
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                gameObject.GetComponentInChildren<ScrollRect>().verticalNormalizedPosition = 1f;
                //VicinityPackBehaviour.CheckVicinity();
            }

            isOpened = !isOpened;
        }
    }
}