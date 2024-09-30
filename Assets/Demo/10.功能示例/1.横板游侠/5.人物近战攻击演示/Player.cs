using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Attack
{
    /// <summary>
    /// ��ҽű�
    /// ���򵥵Ĺ����߼���ʾ��
    /// </summary>
    public class Player : MonoBehaviour
    {
        //����λ��
        [SerializeField] private Transform attackPosition;

        //�����жϷ�Χ
        [SerializeField] private float radiusX;
        [SerializeField] private float radiuxY;

        //��������
        [SerializeField] private Animator animator;

        //�����������ȴʱ��
        [SerializeField] private float attackInterval;
        private float attackCoolDown;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //������ȴ����ܹ���
            attackCoolDown -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && attackCoolDown <= 0)
            {
                attackCoolDown = attackInterval;
                animator.SetBool("IsAttack", true);//���Ź�������
                Attack();
            }
        }

        /// <summary>
        /// �����߼�
        /// </summary>
        private void Attack()
        {
            var colliders = Physics2D.OverlapBoxAll(attackPosition.position, new Vector2(radiusX, radiuxY), 0);
            foreach (var collider in colliders)
            {
                var enemey = collider.GetComponent<Enemy>();
                if (enemey != null)
                    enemey.TakeDamage(1);
            }
        }

        /// <summary>
        /// ���ƹ�����Χ
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(attackPosition.position, new Vector2(radiusX, radiuxY));
        }


        /// <summary>
        /// ������������
        /// </summary>
        public void AttackEnd()
        {
            animator.SetBool("IsAttack", false);
        }
    }
}