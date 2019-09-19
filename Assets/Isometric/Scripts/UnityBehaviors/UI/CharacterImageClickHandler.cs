using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.UnityBehaviours.UI
{
    public class CharacterImageClickHandler : MonoBehaviour
    {
        public GameObject CharacterDetailsPanel;
        private bool isOpened;

        public void ToggleCharacterDetailsPanel()
        {

            if (isOpened)
            {
                CharacterDetailsPanel.transform.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                CharacterDetailsPanel.transform.localScale = new Vector3(1, 1, 1);
            }

            isOpened = !isOpened;
        }
    }
}