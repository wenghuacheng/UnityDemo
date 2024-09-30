using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HB.Operation.Ability
{
    /// <summary>
    /// ����ʵ���࣬��Һ͵��˶�����ʹ��
    /// ������ʾ������ʽ�����Ը���ֻ���ڵ���
    /// </summary>
    public class RPGEntity : MonoBehaviour
    {
        protected Rigidbody2D rb;
        protected Animator anim;
        //���ﳯ��
        protected int facingDir = 1;
        protected bool facingRight = true;

        [Header("��ײ������")]
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