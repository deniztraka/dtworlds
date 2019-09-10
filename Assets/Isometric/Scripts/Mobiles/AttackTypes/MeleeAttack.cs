using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DTWorlds.Interfaces;
using DTWorlds.UnityBehaviours;

namespace DTWorlds.Mobiles.AttackTypes
{
    public class MeleeAttack : IAttackType
    {
        private GameObject weaponSlot;
        private AttackingAnimationHandler animationHandler;
        private bool isAttacking;
        public bool IsAttacking
        {
            get { return isAttacking; }
            set { isAttacking = value; }
        }
        public MeleeAttack(GameObject weaponSlot, AttackingAnimationHandler animationHandler)
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

        public void Attack(int direction)
        {
            isAttacking = true;
            Debug.Log(isAttacking);
            animationHandler.PlayAttackingAnimation(direction, false);
        }

        private void AttackingEnds(){
            isAttacking = false;
        }
    }

}