using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Games.DefendWall
{
    /// <summary>
    /// 弹药
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private LayerMask whatIsEnemy;

        private Vector2 direction = Vector2.up;
        private float speed = 10f;
        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Invoke("DestroySelf", 5f);
        }

        void Update()
        {
            rb.velocity = direction * speed;
        }

        /// <summary>
        /// 设置方向
        /// </summary>
        public void SetDirection(Vector2 direction)
        {
            this.direction = direction;
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