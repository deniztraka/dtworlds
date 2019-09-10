
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

        private float attackSpeedMultiplier;
        private const float minAttackSpeedMultiplier = 1.2f;
        private const float defaultSpeedMultiplier = 1f;

        Animator animator;

        private void Awake()
        {
            //cache the animator component
            animator = GetComponent<Animator>();
        }

        public void SetAttackSpeedMultiplier(float multiplier)
        {
            //attackSpeedMultiplier = multiplier <= minAttackSpeedMultiplier ? minAttackSpeedMultiplier : multiplier;
            attackSpeedMultiplier = multiplier;
        }

        public void PlayAttackingAnimation(int currentDirectionIndex, bool isRanged)
        {
            var animationName = isRanged ? attackingBowDirections[currentDirectionIndex] : attackingDirections[currentDirectionIndex];
            animator.SetFloat("speed", attackSpeedMultiplier);
            animator.Play(animationName);
        }

        public void AttackEnded()
        {
            if (AttackEnds != null)
            {
                animator.SetFloat("speed", defaultSpeedMultiplier);
                AttackEnds.Invoke();
            }
        }
    }
}