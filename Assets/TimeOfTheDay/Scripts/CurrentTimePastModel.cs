using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TimeOfTheDay
{
    public class CurrentTimePastModel
    {
        public int Days;
        public int Hours;
        public int Minutes;
        public int Seconds;

        public CurrentTimePastModel(int days, int hours, int minutes, int seconds)
        {
            Days = days;
            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }
    }
}

