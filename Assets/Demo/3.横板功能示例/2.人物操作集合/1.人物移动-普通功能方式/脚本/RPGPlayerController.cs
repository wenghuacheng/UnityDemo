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

        //������ؿ���
        private RPGPlayerAnimatorController animatorController;
        //������
        private RPGPlayerGroundCheck groundCheck;
        //��̹���
        private RPGPlayerDashController dashController;
        //����
        private RPGPlayerAttackController attackController;

        //���ﳯ��
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

        //�����ƶ�
        private void Movement()
        {
            if (!dashController.IsDashing)
            {
                float x = Input.GetAxisRaw("Horizontal");
                rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
            }
        }

        //��Ծ
        private void Jump()
        {
            if (!Input.GetKeyDown(KeyCode.Space) || !groundCheck.IsOnGround) return;
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        #region ���﷭ת
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
        /// ����
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