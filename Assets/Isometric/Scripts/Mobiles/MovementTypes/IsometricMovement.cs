using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using DTWorlds.UnityBehaviours;
using UnityEngine;

namespace DTWorlds.Mobiles.MovementTypes
{
    public class IsometricMovement : IMovement
    {
        private Rigidbody2D rigidbody;
        private float movementSpeed;
        private GameObject gameObject;

        private MovementAnimationHandler animationHandler;

        private IMovementAxis movementAxis;

        public IMovementAxis MovementAxis { get => movementAxis; }

        public float MovementSpeed
        {
            set
            {
                value = movementSpeed;
            }
        }

        public GameObject GameObject
        {
            set
            {
                value = gameObject;
            }
        }


        //returns currnet direction index
        private int? setCurrentAnimation(Vector2 movement)
        {
            if (animationHandler != null)
            {
                return animationHandler.SetCharacterMovementAnimation(movement, false);
            }

            return null;
        }

        public IsometricMovement(IMovementAxis movementAxis)
        {
            this.movementAxis = movementAxis;
        }

        public void Initialize(GameObject gameObject, float movementSpeed)
        {
            this.gameObject = gameObject;
            this.movementSpeed = movementSpeed;

            this.rigidbody = gameObject.GetComponent<Rigidbody2D>();
            var animationSpriteTransform = gameObject.transform.Find("AnimationSprite");
            if (animationSpriteTransform != null)
            {
                animationHandler = animationSpriteTransform.gameObject.GetComponent<MovementAnimationHandler>();
            }
        }

        //returns current direction index
        public int? Move()
        {
            Vector2 currentPos = rigidbody.position;

            float horizontalInput = movementAxis.GetXAxis();
            float verticalInput = movementAxis.GetYAxis();
            Debug.LogFormat("{0}-{1}", horizontalInput, verticalInput);
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            //inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * movementSpeed * 1f;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            rigidbody.MovePosition(newPos);
            return setCurrentAnimation(movement);
        }


    }
}

