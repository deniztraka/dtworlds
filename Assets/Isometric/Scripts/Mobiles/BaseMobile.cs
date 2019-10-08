using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
using DTWorlds.Mobiles.AttackTypes;
using DTWorlds.UnityBehaviours;
using DTWorlds.Mobiles.DamagableProperties;
using Kryz.CharacterStats;

namespace DTWorlds.Mobiles
{
    public abstract class BaseMobile
    {
        
        public CharacterStat Armor;

        public CharacterStat Strength;

        public CharacterStat Dexterity;

        private GameObject gameObject;
        private float movementSpeed;
        private IMovement movementType;
        private IAttackType attackType;        

        private List<IAttackType> attackTypes;
        private IAttackType currentAttackType;

        public BaseDamagableProperty Health;

        public BaseDamagableProperty Energy;

        public BaseDamagableProperty Hunger;

        public int? CurrentDirection;

        private bool isRunning;

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                if (value)
                {
                    //calculate movement speed in this case running
                    movementSpeed = 2f;
                }
                else
                {
                    //calculate movement speed in this case walking
                    movementSpeed = 1f;
                }
                isRunning = value;
            }
        }

        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
       

        public BaseMobile()
        {
            
        }

        public BaseMobile(GameObject gameObject, float movementSpeed)
        {
            this.gameObject = gameObject;
            this.movementSpeed = movementSpeed;

            Health = new Health(this);
            Energy = new Energy(this);
            Hunger = new Hunger(this);

            Armor = new CharacterStat();
            Dexterity = new CharacterStat();
            Strength = new CharacterStat();

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
                CurrentDirection = this.movementType.Move(movementSpeed);
                
                if(isRunning && !(this.movementType.MovementAxis.GetXAxis() == 0 && this.movementType.MovementAxis.GetYAxis() == 0)){
                    Energy.CurrentValue -= 0.1f;
                }else{
                    Energy.CurrentValue += 0.01f;
                }
            }
        }

        public void Attack()
        {
            this.currentAttackType.Attack(CurrentDirection ?? 0, 1 / GetAttackRate());
        }

        public bool IsAttacking()
        {
            return this.currentAttackType.IsAttacking;
        }

        public void ChangeAttackType(int index)
        {
            this.currentAttackType = this.attackTypes[index];
        }

        public float GetAttackRate()
        {
            //stamina ticks = max stamina / current stamina
            //float attackRate = ((Base Weapon Speed - Stamina Ticks) * (100.0 / (100 + Swing Speed Increase))) always round down
            return 1f;
        }
    }
}

