using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.Common.PlayerSysWithUI
{
    public class PlayerAnimations : MonoBehaviour
    {
        //¶¯»­²ÎÊý
        private readonly int moveX = Animator.StringToHash("MoveX");
        private readonly int moveY = Animator.StringToHash("MoveY");
        private readonly int moving = Animator.StringToHash("Moving");
        private readonly int death = Animator.StringToHash("Death");
        private readonly int revive = Animator.StringToHash("Revive");
        private readonly int attacking = Animator.StringToHash("Attacking");

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }


        public void ShowDeathAnimation()
        {
            animator.SetTrigger(death);
        }

        public void SetMoveBoolTransition(bool value)
        {
            animator.SetBool(moving, value);
        }

        public void SetMoveAnimation(Vector2 dir)
        {
            animator.SetFloat(moveX, dir.x);
            animator.SetFloat(moveY, dir.y);
        }

        public void SetAttackingAnimation(bool value)
        {
            animator.SetBool(attacking, value);
        }

        public void ResetPlayer()
        {
            SetMoveAnimation(Vector2.down);
            animator.SetTrigger(revive);
        }
    }
}