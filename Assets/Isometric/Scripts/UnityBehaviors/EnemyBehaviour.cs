using DTWorlds.Mobiles;
using DTWorlds.Mobiles.MovementTypes;
using DTWorlds.Mobiles.MovementInputs;
using DTWorlds.Mobiles.MovementInputs.Enemy;

namespace DTWorlds.UnityBehaviours
{
    public class EnemyBehaviour : BaseMobileBehaviour
    {        

        public EnemyMovementHandler EnemyMovementHandler;

        private void buildPlayer()
        {
            var player = new Player(gameObject, 1);
            player.SetMovementType(new IsometricMovement(new AIMovementInput(EnemyMovementHandler)));
            player.Health.CurrentValue = 100;
            InitMobile(player);
        }

        void Awake()
        {
            buildPlayer();
        }
    }
}