using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HB.Operation.Ability
{
    /// <summary>
    /// 人物实体类，玩家和敌人都可以使用
    /// 这里演示两个方式，所以该类只用于敌人
    /// </summary>
    public class RPGEntity : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Animator anim;
        //人物朝向
        protected int facingDir = 1;
        protected bool facingRight = true;

        [Header("碰撞体属性")]
        [SerializeField] protected Transform groundCheck;
        [SerializeField] protected float groundCheckDistance;
        [SerializeField] protected LayerMask whatIsGround;

        protected bool isGrounded;

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }


        protected virtual void Update()
        {
            isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        }

        protected virtual void Flip()
        {
            facingDir = facingDir * -1;
            facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - groundCheckDistance));
        }
    }
}