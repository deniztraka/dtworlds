using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.Mobiles.MovementInputs
{
    public class KeyboardMovementInput : IMovementAxis
    {
        public KeyboardMovementInput()
        {

        }
        public float GetXAxis()
        {
            return Input.GetAxis("Horizontal");
        }

        public float GetYAxis()
        {
            return Input.GetAxis("Vertical");
        }
    }
}