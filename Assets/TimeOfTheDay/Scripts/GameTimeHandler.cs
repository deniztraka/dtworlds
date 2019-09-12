using System.Collections;
using UnityEngine;

namespace TimeOfTheDay
{
    public class GameTimeHandler : MonoBehaviour
    {
        public int DayLengthInSeconds;
        public bool isEnabled;
        public delegate void GameTimeEventHandler();
        public event GameTimeEventHandler OnAfterValueChangedEvent;

        private int currentDay;
        private int currentHour;
        private int currentMinute;
        private int currentSecond;
        private float processFrequencyInSeconds = 1;
        public long RealGameSecondsPast;
        private float currentTimeOfDay;

        void Start()
        {
            if (isEnabled)
            {
                Init();
                StartCoroutine(Process());
            }
        }

        internal void SetCurrentTime(long realGameSecondsPast)
        {
            RealGameSecondsPast = realGameSecondsPast;
        }

        private void Init()
        {
            RealGameSecondsPast = 0;
        }

        public CurrentTimePastModel GetGameTime()
        {
            return new CurrentTimePastModel(currentDay, currentHour, currentMinute, currentSecond);
        }

        private IEnumerator Process()
        {
            while (isEnabled)
            {
                yield return new WaitForSeconds((float)processFrequencyInSeconds);
                CalculateTimeOfTheDay();
                RealGameSecondsPast++;
            }
        }

        public void CalculateTimeOfTheDay()
        {
            if (RealGameSecondsPast > 0)
            {

                var ratio = DayLengthInSeconds / 86400f;
                //how many seconds past according to game time.
                var secondsPastInGame = RealGameSecondsPast / ratio;

                var dayx = secondsPastInGame / (24 * 3600);

                secondsPastInGame = secondsPastInGame % (24 * 3600);
                var hourx = secondsPastInGame / 3600;

                secondsPastInGame %= 3600;
                var minutesx = secondsPastInGame / 60;

                secondsPastInGame %= 60;
                var secondsx = secondsPastInGame;

                currentDay = (int)dayx;
                currentHour = (int)hourx;
                currentMinute = (int)minutesx;
                currentSecond = (int)secondsx;

                if (OnAfterValueChangedEvent != null)
                {
                    OnAfterValueChangedEvent();
                }
            }
        }
    }
}