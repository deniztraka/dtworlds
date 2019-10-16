using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeOfTheDay;

namespace DTWorlds.UnityBehaviours
{
    [RequireComponent(typeof(PlayerBehaviour))]
    public class HungerBehaviour : MonoBehaviour
    {
        private PlayerBehaviour playerBehaviour;

        public bool IsEnabled;
        public float Frequency;
        public float ModifyAmount;

        void Start()
        {
            playerBehaviour = GetComponent<PlayerBehaviour>();
            StartCoroutine(DecraseValue());

            if (ModifyAmount == 0)
            {
                var gameTimeObj = GameObject.FindGameObjectWithTag("GameTime");
                var gameTimeHandler = gameTimeObj.GetComponent<GameTimeHandler>();
                ModifyAmount = -playerBehaviour.Mobile.Hunger.MaxValue / gameTimeHandler.DayLengthInSeconds;
            }
        }

        IEnumerator DecraseValue()
        {
            while (true)
            {
                yield return new WaitForSeconds(Frequency);
                if (IsEnabled)
                {
                    playerBehaviour.ModifyHunger(ModifyAmount);
                }
            }
        }
    }

}