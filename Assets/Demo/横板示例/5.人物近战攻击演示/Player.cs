using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Attack
{
    /// <summary>
    /// Íæ¼Ò½Å±¾
    /// ¡¾¼òµ¥µÄ¹¥»÷Âß¼­ÑÝÊ¾¡¿
    /// </summary>
    public class Player : MonoBehaviour
    {
        //¹¥»÷Î»ÖÃ
        [SerializeField] private Transform attackPosition;

        //¹¥»÷ÅÐ¶Ï·¶Î§
        [SerializeField] private float radiusX;
        [SerializeField] private float radiuxY;

        //¶¯»­²Ù×÷
        [SerializeField] private Animator animator;

        //¹¥»÷¼ä¸ôÓëÀäÈ´Ê±¼ä
        [SerializeField] private float attackInterval;
        private float attackCoolDown;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //¹¥»÷ÀäÈ´ºó²ÅÄÜ¹¥»÷
            attackCoolDown -= Time.deltaTime;
            if (Input.GetMouseButtonDown(0) && attackCoolDown <= 0)
            {
                attackCoolDown = attackInterval;
                animator.SetBool("IsAttack", true);//²¥·Å¹¥»÷¶¯»­
                Attack();
            }
        }

        /// <summary>
        /// ¹¥»÷Âß¼­
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
        /// »æÖÆ¹¥»÷·¶Î§
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(attackPosition.position, new Vector2(radiusX, radiuxY));
        }


        /// <summary>
        /// ¹¥»÷¶¯»­½áÊø
        /// </summary>
        public void AttackEnd()
        {
            animator.SetBool("IsAttack", false);
        }
    }
}