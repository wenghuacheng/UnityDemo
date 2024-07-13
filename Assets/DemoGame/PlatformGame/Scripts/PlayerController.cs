using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.DemoGame.PlatformGame.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed;//移速

        [Header("跳跃相关")]
        public float jumpForce;//跳跃力
        public Transform groundCheckPoint;//地面检测点
        public LayerMask whatIsGround;//地面的layer
        private bool isGrounded;
        private bool canDoubleJump;//是否可以二段跳

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

        #region 角色行为

        private void PlayerAction()
        {
            PlayerMovement();
            CheckIsGrounded();
            PlayerJump();
            SetAnimatorParameter();
        }

        /// <summary>
        /// 玩家水平移动
        /// </summary>
        private void PlayerMovement()
        {
            var direction = GetInputDirection();
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);//X方向上的移动
            Flip();
        }

        /// <summary>
        /// 玩家跳跃
        /// </summary>
        private void PlayerJump()
        {
            if (!IsInputJump()) return;


            if (isGrounded)
            {
                //一段跳
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);//Y方向上的移动
            }
            else if (canDoubleJump)
            {
                //二段跳
                canDoubleJump = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);//Y方向上的移动
            }
        }
        #endregion

        #region 玩家输入
        /// <summary>
        /// 获取输入方向【后续改为新的输入系统】
        /// </summary>
        /// <returns></returns>
        private Vector2 GetInputDirection()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            return new Vector2(x, y);
        }

        /// <summary>
        /// 玩家输入跳跃键
        /// </summary>
        /// <returns></returns>
        private bool IsInputJump()
        {
            return Input.GetButtonDown("Jump");
        }

        #endregion

        /// <summary>
        /// 地面检测
        /// </summary>
        /// <returns></returns>
        private bool CheckIsGrounded()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

            if (isGrounded)
                canDoubleJump = true;//标记是否可以二段跳

            return isGrounded;
        }

        /// <summary>
        /// 设置动画参数
        /// </summary>
        private void SetAnimatorParameter()
        {
            animator.SetFloat("moveSpeed", Mathf.Abs(rb.velocity.x));
            animator.SetBool("isGrounded", isGrounded);
        }

        /// <summary>
        /// 反转玩家
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