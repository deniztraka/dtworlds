using UnityEngine;

namespace DTWorlds.Mobiles
{
    public class Player : Human
    {

        public Player(GameObject animationSprite, float movementSpeed) : base(animationSprite, movementSpeed)
        {

        }

        public override void OnBeforeAttacking()
        {
            base.OnBeforeAttacking();

            //fing something ITargetable on the direction of the user
            //then call ITargetable.attack            
        }
    }
}
