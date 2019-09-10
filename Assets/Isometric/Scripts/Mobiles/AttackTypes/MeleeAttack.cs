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

        public MeleeAttack(GameObject weaponSlot, AttackingAnimationHandler animationHandler)
        {
            this.animationHandler = animationHandler;
            this.weaponSlot = weaponSlot;
        }

        public GameObject WeaponSlot
        {
            get { return weaponSlot; }
            set { weaponSlot = value; }
        }

        public void Attack(int direction)
        {
            Debug.Log("MeleeAttack");
            animationHandler.PlayAttackingAnimation(direction, false);
        }
    }

}