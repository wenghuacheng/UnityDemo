using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Games.DefendWall
{
    /// <summary>
    /// 跟踪子弹
    /// </summary>
    public class AIBullet : MonoBehaviour
    {
        private Enemy enemy;
        private float speed = 10f;
        private Rigidbody2D rb;

        private Vector2 direction;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Invoke("DestroySelf", 5f);
        }

        void Update()
        {
            //if (enemy != null && !enemy.IsDestroyed())
            //    direction = (enemy.transform.position -this.transform.position).normalized;
            rb.velocity = direction * speed;
        }

        public void SetEnemy(Enemy enemy)
        {
            this.enemy = enemy;
            direction = (enemy.transform.position - this.transform.position).normalized;
        }

        /// <summary>
        /// 销毁自身
        /// </summary>
        private void DestroySelf()
        {
            if (this.gameObject.IsDestroyed()) return;
            Destroy(this.gameObject);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}