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

        public RangeAttack(GameObject weaponSlot, AttackingAnimationHandler animationHandler)
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
            Debug.Log("RangeAttack");
            animationHandler.PlayAttackingAnimation(direction, true);
        }
    }

}