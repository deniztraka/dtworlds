using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TimeOfTheDay
{
    public class IconBehaviour : UIBehaviour
    {
        float targetAngle = 360;
        //float turnSpeed = 0.1f;

        void Start()
        {
            //turnSpeed = TimeOfTheDay.DayLengthInSeconds / 86400f;
        }

        public override void UpdateMe()
        {
            var currentGameTime = GameTime.GetGameTime();
            targetAngle = (currentGameTime.Hours * 360) / 24;
        }

        void Update()
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), Time.deltaTime);
        }
    }
}
