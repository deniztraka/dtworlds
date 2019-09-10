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
        float attackRate = 0.5f;
        float nextAttack = 0;

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

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            player.Move();
        }

        public void Attack()
        {
            if (Time.time > nextAttack)
            {
                nextAttack = Time.time + attackRate;
                player.Attack();
            }
        }


    }
}