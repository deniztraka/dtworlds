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

        private void buildPlayer()
        {
            player = new Player(gameObject, 1);
            player.SetMovementType(new IsometricMovement(new JoyStickMovementInput(joystick)));
        }

        // Start is called before the first frame update
        void Start()
        {
            buildPlayer();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            player.Move();
        }

        public void Attack()
        {
            player.Attack();
        }


    }
}