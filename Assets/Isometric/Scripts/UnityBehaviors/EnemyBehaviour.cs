using DTWorlds.Mobiles;
using DTWorlds.Mobiles.MovementTypes;
using DTWorlds.Mobiles.MovementInputs;
using DTWorlds.Mobiles.MovementInputs.Enemy;
using UnityEngine;

namespace DTWorlds.UnityBehaviours
{
    public class EnemyBehaviour : BaseMobileBehaviour
    {
        private GameObject playerGameObject;
        public EnemyMovementHandler EnemyMovementHandler;

        private void build()
        {
            var human = new Human(gameObject, 1);
            human.SetMovementType(new IsometricMovement(new AIMovementInput(EnemyMovementHandler)));
            human.Health.CurrentValue = 100;
            InitMobile(human);

            //EnemyMovementHandler.Input = new Vector2(1,1);

            playerGameObject = GameObject.FindGameObjectWithTag("Player");
        }

        void Awake()
        {
            build();
        }

        public bool Follow(GameObject target)
        {
            var distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance > this.Mobile.AttackRange)
            {
                this.SetRunning(false);
                EnemyMovementHandler.Follow(target);
                return true;
            }

            EnemyMovementHandler.Stop();
            return false;
        }

        //TODO Koşması lazım. SetRunning bir kere set edilmesi lazım.
        public void Chase(GameObject target)
        {
            var distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance > this.Mobile.AttackRange + 1f)
            {
                EnemyMovementHandler.Follow(target);
            }
            else
            {
                Follow(target);
            }
        }

        public void Attack(GameObject target)
        {
            if (!Follow(target))
            {
                SetAttacking(true);
            }
        }

        public override void Update()
        {
            base.Update();
            Attack(playerGameObject);
        }
    }
}