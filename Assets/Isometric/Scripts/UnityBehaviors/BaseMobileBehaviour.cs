using System.Collections;
using System.Collections.Generic;
using DTWorlds.Mobiles;
using DTWorlds.Mobiles.MovementTypes;
using DTWorlds.Mobiles.MovementInputs;
using UnityEngine;
using DTWorlds.Items.Equipments;
using System;
using DTWorlds.Items.Inventory.Behaviours;
using DTWorlds.Items;
using DTWorlds.Interfaces;
using DTWorlds.TileMap;

namespace DTWorlds.UnityBehaviours
{
    public abstract class BaseMobileBehaviour : MonoBehaviour
    {        
        private TileObjectBehaviour target;

        private BaseMobile mobile;

        public BaseMobile Mobile
        {
            get { return mobile; }
        }

        public void SetTarget(TileObjectBehaviour target)
        {
            this.mobile.Target = null;

            if(this.target != null){
                this.target.SetSelected(false);                
            }

            if(target == null){
                this.target = null;
                this.mobile.Target = null;
                return;
            }

            // Debug.Log("asd"); 
            // this.target = target.GetComponent<TileObjectBehaviour>();
            // mobile.Target = this.target;

            this.target = target;
            this.target.SetSelected(true);
            this.mobile.Target = target;
        }

        public void InitMobile(BaseMobile mobile)
        {
            this.mobile = mobile;
        }

        float nextAttack = 0;

        private bool isAtacking = false;
        public void SetAttacking(bool state)
        {
            this.isAtacking = state;
        }

        void Update()
        {
            if (isAtacking)
            {
                Attack();
            }
        }

        void FixedUpdate()
        {
            mobile.Move();
        }

        private void Attack()
        {
            //attackRate = ((Base Weapon Speed - Stamina Ticks) * (100.0 / (100 + Swing Speed Increase))) always round down
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + mobile.GetAttackRate();
                mobile.Attack();
            }
        }

        public void SetRunning(bool val)
        {
            mobile.IsRunning = val;
        }

        public void ModifyHunger(float amount)
        {
            mobile.Hunger.CurrentValue += amount;
        }
    }
}