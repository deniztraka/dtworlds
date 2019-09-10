
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace DTWorlds.UnityBehaviours
{
    public class AttackingAnimationHandler : MonoBehaviour
    {
        public static readonly string[] attackingDirections = { "human_base_m_attacking_n", "human_base_m_attacking_nw", "human_base_m_attacking_w", "human_base_m_attacking_sw", "human_base_m_attacking_s", "human_base_m_attacking_se", "human_base_m_attacking_e", "human_base_m_attacking_ne" };
        public static readonly string[] attackingBowDirections = { "human_base_m_attackingBow_n", "human_base_m_attackingBow_nw", "human_base_m_attackingBow_w", "human_base_m_attackingBow_sw", "human_base_m_attackingBow_s", "human_base_m_attackingBow_se", "human_base_m_attackingBow_e", "human_base_m_attackingBow_ne" };

        public UnityEvent AttackEnds;

        Animator animator;

        private void Awake()
        {
            //cache the animator component
            animator = GetComponent<Animator>();
        }

        public void PlayAttackingAnimation(int currentDirectionIndex, bool isRanged)
        {
            animator.Play(isRanged ? attackingBowDirections[currentDirectionIndex] : attackingDirections[currentDirectionIndex]);
        }

        public void AttackEnded()
        {
            if (AttackEnds != null)
            {
                AttackEnds.Invoke();
            }
        }
    }
}