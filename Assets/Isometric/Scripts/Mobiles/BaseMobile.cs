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
using UnityEngine.Tilemaps;

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
                    movementSpeed = 1.25f;
                }
                else
                {
                    //calculate movement speed in this case walking
                    movementSpeed = 1f;
                }
                isRunning = value;
            }
        }

        public bool IsMoving
        {
            get
            {
                return !(this.movementType.MovementAxis.GetXAxis() == 0 && this.movementType.MovementAxis.GetYAxis() == 0);
            }
        }

        public MobileInventory Inventory;

        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
        public IInteractable Target { get; internal set; }

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

            Inventory = new MobileInventory(this);

            //setting up attacking system.
            var animationSpriteTransform = gameObject.transform.Find("AnimationSprite");
            if (animationSpriteTransform != null)
            {
                var attackingAnimationHandler = animationSpriteTransform.gameObject.GetComponent<AttackingAnimationHandler>();
                this.attackTypes = new List<IAttackType>();
                var meleeAttack = new MeleeAttack(null, attackingAnimationHandler);
                meleeAttack.OnAfterAttacked += new MeleeAttack.AttackStateEventHandler(OnAfterAttacked);
                meleeAttack.OnBeforeAttacking += new MeleeAttack.AttackStateEventHandler(OnBeforeAttacking);

                var rangedAttack = new RangeAttack(null, attackingAnimationHandler);
                this.attackTypes.Add(meleeAttack);
                this.attackTypes.Add(rangedAttack);
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

        public virtual void OnAfterAttacked()
        {
            if (Target != null)
            {
                Target.Interact(this);
            }
            //Debug.Log("on after attacked");

            //Debug.Log(this.CurrentDirection);

            Vector3 direction = new Vector3(0, 0);
            //float distance = 0;
            switch (this.CurrentDirection)
            {
                case 0:
                    //distance = 0.25f;
                    direction = new Vector3(1, 0);
                    break;
                case 1:
                    //distance = 0.5f;
                    direction = new Vector3(1, 1);
                    break;
                case 2:
                    //distance = 0.25f;
                    direction = new Vector3(0, 1);
                    break;
                case 3:
                    //distance = 0.5f;
                    direction = new Vector3(-1, 1);
                    break;
                case 4:
                    //distance = 0.25f;
                    direction = new Vector3(-1, 0);
                    break;
                case 5:
                    //distance = 0.5f;
                    direction = new Vector3(-1, -1);
                    break;
                case 6:
                    //distance = 0.25f;
                    direction = new Vector3(0, -1);
                    break;
                case 7:
                    //distance = 0.5f;
                    direction = new Vector3(1, -1);
                    break;
            }

            var currPos = this.gameObject.transform.position;
            var mobileMask = LayerMask.GetMask("Mobiles");

            var floorObject = GameObject.FindGameObjectWithTag("Floor");
            var floorTileMap = floorObject.GetComponent<Tilemap>();
            var attackerCell = floorTileMap.WorldToCell(currPos);
            var targetCell = attackerCell + direction;

            var ceiled = Vector3Int.FloorToInt(targetCell);

            var targetPoint = floorTileMap.GetCellCenterWorld(ceiled);

            // Debug.Log(direction);
            // Debug.Log(attackerCell);
            // Debug.Log(targetCell);
            // Debug.Log(targetPoint);
            // Debug.DrawLine(currPos, targetPoint, Color.yellow, 5);
            RaycastHit2D[] results = new RaycastHit2D[10];
            var hit = Physics2D.LinecastNonAlloc(currPos, targetPoint, results, mobileMask);
            for (int i = 0; i < hit; i++)
            {
                var collidedObj = results[i];
                if (collidedObj.collider != null && collidedObj.collider.gameObject != this.gameObject)
                {
                    var enemyBehaviour = collidedObj.collider.gameObject.GetComponent<EnemyBehaviour>();
                    if (enemyBehaviour != null)
                    {
                        enemyBehaviour.Mobile.TakeDamage(this);
                        //Debug.Log("Enemy hit");
                        break;
                    }
                }
            }
        }

        private void TakeDamage(BaseMobile fromMobile)
        {
            var rawDamage = fromMobile.GetRawDamage();
            this.Health.CurrentValue -= rawDamage;
            Debug.Log(fromMobile.gameObject.name + " took " + rawDamage + " damage from " + this.gameObject.name);
        }

        private float GetRawDamage()
        {
            return this.Strength.Value / 2;
        }

        public virtual void OnBeforeAttacking()
        {
            //Debug.Log("before attack");
        }
    }
}

