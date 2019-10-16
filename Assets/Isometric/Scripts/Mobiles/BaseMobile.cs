using System.Collections;
using System.Collections.Generic;
using DTWorlds.Interfaces;
using UnityEngine;
using DTWorlds.Mobiles.AttackTypes;
using DTWorlds.UnityBehaviours;
using DTWorlds.Mobiles.DamagableProperties;
using Kryz.CharacterStats;
using System;
using DTWorlds.Items.Inventory.Models;

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

        public MobileInventory Inventory;

        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }


        public BaseMobile()
        {

        }

        public BaseMobile(GameObject gameObject, float movementSpeed)
        {
            this.gameObject = gameObject;
            this.movementSpeed = movementSpeed;

            Armor = new CharacterStat();
            Dexterity = new CharacterStat();
            Dexterity.BaseValue = 10;
            Strength = new CharacterStat();
            Strength.BaseValue = 10;

            Health = new Health(this);
            Energy = new Energy(this);
            Hunger = new Hunger(this);



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

                //TODO: Energy lowers quicker when running
                if (isRunning && !(this.movementType.MovementAxis.GetXAxis() == 0 && this.movementType.MovementAxis.GetYAxis() == 0))
                {
                    Energy.CurrentValue -= 0.1f;
                }
                else
                {
                    Energy.CurrentValue += 0.01f;
                    //Health.CurrentValue += 0.1f;
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
            //weapon speed should be between 1.5 and 3.5 - lower is quicker
            //swing speed increase should be between 0 and 60 - higher is quicker
            //min attack rate 0.3sn
            //max attack rate 2.5sn

            float weaponSpeed = 2f;
            float swingSpeedIncrease = 0f;

            float energyTicks = Energy.CurrentValue / Energy.MaxValue;
            var calculatedAttackRate = ((Mathf.Clamp(weaponSpeed, 1.5f, 3.5f) - energyTicks) * ((float)100.0 / ((float)100.0 + swingSpeedIncrease)));

            return calculatedAttackRate;

        }
    }
}

