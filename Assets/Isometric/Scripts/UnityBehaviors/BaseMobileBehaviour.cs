using System.Collections;
using System.Collections.Generic;
using DTWorlds.Mobiles;
using DTWorlds.Mobiles.MovementTypes;
using DTWorlds.Mobiles.MovementInputs;
using UnityEngine;
using InventorySystem;
using DTWorlds.Items.Equipments;
using System;
using InventorySystem.UI;

namespace DTWorlds.UnityBehaviours
{
    public abstract class BaseMobileBehaviour : MonoBehaviour
    {
        public FixedJoystick joystick;

        public InventoryBehaviour InventoryBehaviour;

        private BaseMobile mobile;

        public BaseMobile Mobile
        {
            get { return mobile; }
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