using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    /// <summary>
    /// ���/���˻���
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] protected Rigidbody2D rb;
        [SerializeField] protected EntityVisual entityVisual;

        [Header("������")]
        [SerializeField] protected Transform groundCheckTransform;
        [SerializeField] protected LayerMask whatIsGround;
        [SerializeField] protected float groundCheckDistance = 0.1f;

        [Header("ǽ����")]
        [SerializeField] protected Transform wallCheckTransform;
        [SerializeField] protected float wallCheckDistance = 0.1f;

        [Header("������Χ")]
        [SerializeField] protected Transform attackPosition;
        [SerializeField] protected float attackDistance = 2f;
        [SerializeField] protected float strikeFlyDuration = 0.5f;//����ʱ��
        [SerializeField] protected Vector2 strikeFlyStrength;//�������ȡ�ˮƽ�ʹ�ֱ��

        //�Ƿ������ұ�
        protected bool isFaceRight = true;

        #region ��������
        public Animator Animator { get { return animator; } }

        public Rigidbody2D Rb { get { return rb; } }

        //�������淽��
        public Vector3 Right
        {
            get
            {
                //��������ҷ�����Ϊ��������ת����Ҫת����Ϊ��������ϵ
                return transform.TransformDirection(Vector3.right);
            }
        }
        #endregion

        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        public bool IsOnGround()
        {
            var hit2D = Physics2D.Raycast(this.groundCheckTransform.position, Vector2.down, groundCheckDistance, whatIsGround);
            return hit2D.collider != null;
        }

        /// <summary>
        /// ǽ����
        /// </summary>
        /// <returns></returns>
        public bool IsWallDetected()
        {
            //��������ҷ�������޸�
            var hit2D = Physics2D.Raycast(this.wallCheckTransform.position, Right, wallCheckDistance, whatIsGround);
            return hit2D.collider != null;
        }

        #region ���﷭ת
        /// <summary>
        /// ��ת����
        /// </summary>
        protected void FlipController()
        {
            if (rb.velocity.x > 0 && !isFaceRight)
            {
                Flip();
                isFaceRight = true;
            }
            else if (rb.velocity.x < 0 && isFaceRight)
            {
                Flip();
                isFaceRight = false;
            }

        }

        /// <summary>
        /// ��ת
        /// </summary>
        public void Flip()
        {
            this.transform.Rotate(0, 180, 0);
        }
        #endregion

        #region ����&������
        /// <summary>
        /// ����
        /// </summary>
        public abstract void Attack();


        /// <summary>
        /// ������
        /// </summary>
        public void Damage(Entity entity)
        {
            entityVisual.FlashAsync();
            StartCoroutine(StrikeFly(entity.Right));
        }

        /// <summary>
        /// ������
        /// </summary>
        protected IEnumerator StrikeFly(Vector2 direction)
        {
            this.rb.velocity = new Vector2(direction.x * strikeFlyStrength.x, strikeFlyStrength.y);
            yield return new WaitForSeconds(strikeFlyDuration);
        }
        #endregion


        #region ����
        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.groundCheckTransform.position, this.groundCheckTransform.position + Vector3.down * groundCheckDistance);
            Gizmos.DrawLine(this.wallCheckTransform.position, this.wallCheckTransform.position + Vector3.right * wallCheckDistance);

            Gizmos.DrawWireSphere(attackPosition.transform.position, attackDistance);
        }
        #endregion
    }
}