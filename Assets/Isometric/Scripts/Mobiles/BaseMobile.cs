using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
using DTWorlds.Mobiles.AttackTypes;
using DTWorlds.UnityBehaviours;
namespace DTWorlds.Mobiles
{
    public abstract class BaseMobile
    {
        private GameObject gameObject;
        private float movementSpeed;
        private IMovement movementType;
        private IAttackType attackType;

        private List<IAttackType> attackTypes;
        private IAttackType currentAttackType;

        public int? CurrentDirection;

        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

        public BaseMobile()
        {
        }

        public BaseMobile(GameObject gameObject, float movementSpeed)
        {
            this.gameObject = gameObject;
            this.movementSpeed = movementSpeed;

            //setting up attacking system.
            var animationSpriteTransform = gameObject.transform.Find("AnimationSprite");
            if (animationSpriteTransform != null)
            {
                var attackingAnimationHandler = animationSpriteTransform.gameObject.GetComponent<AttackingAnimationHandler>();
                this.attackTypes = new List<IAttackType>();
                this.attackTypes.Add(new MeleeAttack(null, attackingAnimationHandler));
                this.attackTypes.Add(new RangeAttack(null, attackingAnimationHandler));
                this.currentAttackType = this.attackTypes[0];
            }
        }

        public void SetMovementType(IMovement movementType)
        {
            movementType.Initialize(this.gameObject, this.movementSpeed);
            this.movementType = movementType;
        }

        public void Move()
        {
            if (!this.currentAttackType.IsAttacking)
            {
                CurrentDirection = this.movementType.Move();
            }
        }

        public void Attack()
        {            
            this.currentAttackType.Attack(CurrentDirection ?? 0, 1/GetAttackRate());
        }

        public bool IsAttacking()
        {
            return this.currentAttackType.IsAttacking;
        }

        public void ChangeAttackType(int index)
        {
            this.currentAttackType = this.attackTypes[index];
        }

        public float GetAttackRate(){
            //stamina ticks =  current stamina / max stamina
            //float attackRate = ((Base Weapon Speed - Stamina Ticks) * (100.0 / (100 + Swing Speed Increase))) always round down
            return 1f;
        }
    }
}

