using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.Ability
{
    public class RPGPlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private const string VelocityKey = "Velocity";
        private const string MovingKey = "IsMoving";
        private const string IsGroundKey = "IsGround";
        private const string IsDashKey = "IsDash";
        private const string yVelocityKey = "yVelocity";//用于判断跳跃时上升还是下降
        private const string IsAttackingKey = "IsAttacking";
        private const string ComboCountKey = "ComboCount";

        public void SetMoveVelocity(float velocity)
        {
            animator.SetBool(MovingKey, velocity != 0);
        }

        public void SetIsGround(bool isGrounded)
        {
            animator.SetBool(IsGroundKey, isGrounded);
        }

        public void SetJumpVelocity(float yVelocity)
        {
            animator.SetFloat(yVelocityKey, yVelocity);
        }

        public void SetIsDash(bool isDash)
        {
            animator.SetBool(IsDashKey, isDash);
        }

        public void SetIsAttack(bool isAttack)
        {
            animator.SetBool(IsAttackingKey, isAttack);
        }

        public void SetCombxCount(int comboCount)
        {
            animator.SetInteger(ComboCountKey, comboCount);
        }
    }
}
