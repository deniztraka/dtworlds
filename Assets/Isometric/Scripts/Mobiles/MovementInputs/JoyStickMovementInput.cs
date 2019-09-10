using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DTWorlds.Mobiles.MovementInputs
{
    public class JoyStickMovementInput : IMovementAxis
    {
        FixedJoystick joystick;
        public JoyStickMovementInput(FixedJoystick joystick)
        {
            this.joystick = joystick;
        }
        public float GetXAxis()
        {
            return joystick.Horizontal;
        }

        public float GetYAxis()
        {
            return joystick.Vertical;
        }
    }
}