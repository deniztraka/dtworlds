using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTWorlds.Interfaces;
using DTWorlds.UnityBehaviours;

namespace DTWorlds.Mobiles.AttackTypes
{
    public class RangeAttack : IAttackType
    {
        private GameObject weaponSlot;
        private AttackingAnimationHandler animationHandler;
        private bool isAttacking;

        public bool IsAttacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }

        public RangeAttack(GameObject weaponSlot, AttackingAnimationHandler animationHandler)
        {
            this.animationHandler = animationHandler;
            this.weaponSlot = weaponSlot;
            animationHandler.AttackEnds.AddListener(AttackingEnds);
        }

        public GameObject WeaponSlot
        {
            get { return weaponSlot; }
            set { weaponSlot = value; }
        }

        public void Attack(int direction, float speedMultiplier)
        {
            animationHandler.SetAttackSpeedMultiplier(speedMultiplier);
            animationHandler.PlayAttackingAnimation(direction, true);
        }

        private void AttackingEnds()
        {
            isAttacking = false;
        }
    }

}