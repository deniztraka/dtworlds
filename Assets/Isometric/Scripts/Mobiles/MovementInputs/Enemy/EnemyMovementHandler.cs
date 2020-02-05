using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.Mobiles.MovementInputs.Enemy
{
    public class EnemyMovementHandler : BaseAIMovementHandler
    {

        private int Sign(float number)
        {
            return number < 0 ? -1 : (number > 0 ? 1 : 0);
        }
        public EnemyMovementHandler()
        {

        }

        private void SetMovementInput(Vector2 vector)
        {
            Input = vector;
        }

        internal void Follow(GameObject target)
        {
            var targetPosition = target.transform.position;
            var currentPosition = this.transform.position;

            var difference = targetPosition - currentPosition;           
            SetMovementInput(difference);            
        }

        internal void Stop()
        {
            SetMovementInput(new Vector2(0, 0));
        }
    }
}