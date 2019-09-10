using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
namespace DTWorlds.Mobiles
{
    public abstract class BaseMobile
    {
        private GameObject gameObject;     
        private float movementSpeed;
        private IMovement movementType;

        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }        

        public BaseMobile()
        {
        }

        public BaseMobile(GameObject gameObject, float movementSpeed)
        {
            this.gameObject = gameObject;
            this.movementSpeed = movementSpeed;
            
        }

        public void SetMovementType(IMovement movementType){
            movementType.Initialize(this.gameObject, this.movementSpeed);           
            this.movementType = movementType;            
        }

        public void Move(){
            this.movementType.Move();
        }


    }
}

