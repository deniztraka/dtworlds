using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DTWorlds.Mobiles.MovementInputs
{
    public class AIMovementInput : IMovementAxis
    {
        BaseAIMovementHandler baseAIMovementHandler;
        public AIMovementInput(BaseAIMovementHandler aIMovementHandler)
        {
            this.baseAIMovementHandler = aIMovementHandler;
        }
        public float GetXAxis()
        {
            return baseAIMovementHandler.Horizontal;
        }

        public float GetYAxis()
        {
            return baseAIMovementHandler.Vertical;
        }
    }
}