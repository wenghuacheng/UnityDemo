using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DemoGame.PlatformGame.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;//����

        [Header("��Ծ���")]
        public float jumpForce;//��Ծ��
        public Transform groundCheckPoint;//�������
        public LayerMask whatIsGround;//�����layer
        private bool isGrounded;
        private bool canDoubleJump;//�Ƿ���Զ�����

        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer sprite;


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sprite = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerAction();
        }

        #region ��ɫ��Ϊ

        private void PlayerAction()
        {
            PlayerMovement();
            CheckIsGrounded();
            PlayerJump();
            SetAnimatorParameter();
        }

        /// <summary>
        /// ���ˮƽ�ƶ�
        /// </summary>
        private void PlayerMovement()
        {
            var direction = GetInputDirection();
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);//X�����ϵ��ƶ�
            Flip();
        }

        /// <summary>
        /// �����Ծ
        /// </summary>
        private void PlayerJump()
        {
            if (!IsInputJump()) return;


            if (isGrounded)
            {
                //һ����
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);//Y�����ϵ��ƶ�
            }
            else if (canDoubleJump)
            {
                //������
                canDoubleJump = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);//Y�����ϵ��ƶ�
            }
        }
        #endregion

        #region �������
        /// <summary>
        /// ��ȡ���뷽�򡾺�����Ϊ�µ�����ϵͳ��
        /// </summary>
        /// <returns></returns>
        private Vector2 GetInputDirection()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            return new Vector2(x, y);
        }

        /// <summary>
        /// ���������Ծ��
        /// </summary>
        /// <returns></returns>
        private bool IsInputJump()
        {
            return Input.GetButtonDown("Jump");
        }

        #endregion

        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        private bool CheckIsGrounded()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

            if (isGrounded)
                canDoubleJump = true;//����Ƿ���Զ�����

            return isGrounded;
        }

        /// <summary>
        /// ���ö�������
        /// </summary>
        private void SetAnimatorParameter()
        {
            animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
            animator.SetBool("isGrounded", isGrounded);
        }

        /// <summary>
        /// ��ת���
        /// </summary>
        private void Flip()
        {
            if (rb.velocity.x < 0)
                sprite.flipX = true;
            else if (rb.velocity.x > 0)
                sprite.flipX = false;
        }
    }
}