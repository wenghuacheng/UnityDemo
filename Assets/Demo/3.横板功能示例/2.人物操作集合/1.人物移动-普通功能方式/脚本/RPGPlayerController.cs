using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.Ability
{
    public class RPGPlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float jumpSpeed = 10;

        //动画相关控制
        private RPGPlayerAnimatorController animatorController;
        //地面检测
        private RPGPlayerGroundCheck groundCheck;
        //冲刺功能
        private RPGPlayerDashController dashController;
        //攻击
        private RPGPlayerAttackController attackController;

        //人物朝向
        private bool isFaceRight = true;

        private void Awake()
        {
            animatorController = GetComponent<RPGPlayerAnimatorController>();
            groundCheck = GetComponent<RPGPlayerGroundCheck>();
            dashController = GetComponent<RPGPlayerDashController>();
            attackController = GetComponent<RPGPlayerAttackController>();
        }


        // Update is called once per frame
        void Update()
        {
            Movement();
            Jump();
            FlipController();
            AnimatorController();
        }

        //人物移动
        private void Movement()
        {
            if (!dashController.IsDashing)
            {
                float x = Input.GetAxisRaw("Horizontal");
                rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
            }
        }

        //跳跃
        private void Jump()
        {
            if (!Input.GetKeyDown(KeyCode.Space) || !groundCheck.IsOnGround) return;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        #region 人物翻转
        private void Flip()
        {
            this.transform.Rotate(0, 180, 0);
        }

        private void FlipController()
        {
            if (rb.velocity.x < 0 && isFaceRight)
            {
                isFaceRight = false;
                Flip();
            }
            else if (rb.velocity.x > 0 && !isFaceRight)
            {
                isFaceRight = true;
                Flip();
            }
        }
        #endregion

        /// <summary>
        /// 动画
        /// </summary>
        private void AnimatorController()
        {
            animatorController.SetMoveVelocity(rb.velocity.x);
            animatorController.SetIsGround(groundCheck.IsOnGround);
            animatorController.SetJumpVelocity(rb.velocity.y);
            animatorController.SetIsDash(dashController.IsDashing);
            animatorController.SetIsAttack(attackController.IsAttack);
            animatorController.SetCombxCount(attackController.ComboCount);
        }
    }
}