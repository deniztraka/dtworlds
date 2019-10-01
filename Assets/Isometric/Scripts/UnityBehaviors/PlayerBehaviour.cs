﻿using System.Collections;
using System.Collections.Generic;
using DTWorlds.Mobiles;
using DTWorlds.Mobiles.MovementTypes;
using DTWorlds.Mobiles.MovementInputs;
using UnityEngine;
using InventorySystem;

namespace DTWorlds.UnityBehaviours
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public FixedJoystick joystick;

        public InventoryBehaviour InventoryBehaviour;

        private Player player;

        public Player Player
        {
            get { return player; }
        }

        float nextAttack = 0;

        private bool isAtacking = false;
        public void SetAttacking(bool state)
        {
            this.isAtacking = state;
        }

        private void buildPlayer()
        {
            player = new Player(gameObject, 1);
            player.SetMovementType(new IsometricMovement(new JoyStickMovementInput(joystick)));
            //player.SetMovementType(new IsometricMovement(new KeyboardMovementInput()));
        }

        void Awake()
        {
            buildPlayer();
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
            player.Move();
        }

        private void Attack()
        {
            //attackRate = ((Base Weapon Speed - Stamina Ticks) * (100.0 / (100 + Swing Speed Increase))) always round down
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + player.GetAttackRate();
                player.Attack();
            }
        }

        public void SetRunning(bool val)
        {
            player.IsRunning = val;
        }

        public void ModifyHunger(float amount)
        {
            player.Hunger.CurrentValue += amount;
        }
    }
}