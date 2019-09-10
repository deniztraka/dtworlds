using System.Collections;
using System.Collections.Generic;
using DTWorlds.Mobiles;
using DTWorlds.Mobiles.MovementTypes;
using DTWorlds.Mobiles.MovementInputs;
using UnityEngine;

namespace DTWorlds.UnityBehaviours
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public FixedJoystick joystick;

        private Player player;
        
        float nextAttack = 0;

        private bool isAtacking = false;
        public void SetAttacking(bool state){
            this.isAtacking = state;
        }

        private void buildPlayer()
        {
            player = new Player(gameObject, 1);
            player.SetMovementType(new IsometricMovement(new JoyStickMovementInput(joystick)));
            //player.SetMovementType(new IsometricMovement(new KeyboardMovementInput()));
        }

        // Start is called before the first frame update
        void Start()
        {
            buildPlayer();
        }

        void Update()
        {
            if(isAtacking){
                Attack();
            }
        }

        // Update is called once per frame
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


    }
}