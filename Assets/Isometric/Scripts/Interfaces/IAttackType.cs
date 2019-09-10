using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DTWorlds.Interfaces
{
    public interface IAttackType
    {
        GameObject WeaponSlot { get; set; }
        bool IsAttacking { get; }

        void Attack(int direction, float attackSpeedMultiplier);
    }
}

