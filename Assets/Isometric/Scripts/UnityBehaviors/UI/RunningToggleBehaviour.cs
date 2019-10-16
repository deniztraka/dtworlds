
using System.Collections;
using System.Collections.Generic;
using DTWorlds.Mobiles;
using DTWorlds.Mobiles.MovementTypes;
using DTWorlds.Mobiles.MovementInputs;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace DTWorlds.UnityBehaviours.UI
{
    public class RunningToggleBehaviour : MonoBehaviour
    {
        private PlayerBehaviour playerBehaviour;

        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; }
        }

        public UnityEvent RunningTogglePressed;

        public Sprite RunningSprite;
        public Sprite WalkingSprite;

        private void Start()
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                this.playerBehaviour = playerObj.GetComponent<PlayerBehaviour>();
            }
        }

        void Update()
        {
            if(this.playerBehaviour.Mobile.Energy.CurrentValue <= 0 && isRunning){
                Toggle();
            }
        }

        public void Toggle()
        {


            var image = gameObject.GetComponent<Image>();
            isRunning = !isRunning;
            if (isRunning)
            {
                image.sprite = RunningSprite;
            }
            else
            {
                image.sprite = WalkingSprite;
            }

            if (playerBehaviour != null)
            {
                playerBehaviour.SetRunning(isRunning);
            }
        }

    }
}