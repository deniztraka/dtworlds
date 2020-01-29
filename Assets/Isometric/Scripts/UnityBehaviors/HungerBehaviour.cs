using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeOfTheDay;

namespace DTWorlds.UnityBehaviours
{
    [RequireComponent(typeof(BaseMobileBehaviour))]
    public class HungerBehaviour : MonoBehaviour
    {
        private BaseMobileBehaviour baseMobileBehaviour;

        public bool IsEnabled;
        public float Frequency;
        public float ModifyAmount;

        void Start()
        {
            baseMobileBehaviour = GetComponent<BaseMobileBehaviour>();
            StartCoroutine(DecraseValue());

            if (ModifyAmount == 0)
            {
                var gameTimeObj = GameObject.FindGameObjectWithTag("GameTime");
                var gameTimeHandler = gameTimeObj.GetComponent<GameTimeHandler>();
                ModifyAmount = -baseMobileBehaviour.Mobile.Hunger.MaxValue / gameTimeHandler.DayLengthInSeconds;
            }
        }

        IEnumerator DecraseValue()
        {
            while (true)
            {
                yield return new WaitForSeconds(Frequency);
                if (IsEnabled)
                {
                    baseMobileBehaviour.ModifyHunger(ModifyAmount);
                }
            }
        }
    }

}