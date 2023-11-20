using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Platform
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Platform _curPlatform;


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Movement();
            Jump();
            Drop();

        }

        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(x * 5, rb.velocity.y);
        }

        private void Jump()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0, 8), ForceMode2D.Impulse);
            }
        }

        /// <summary>
        /// 从平台上落下
        /// </summary>
        public void Drop()
        {
            if (!Input.GetKeyDown(KeyCode.DownArrow) || _curPlatform == null) return;
            _curPlatform.Drop();
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            _curPlatform = collision.gameObject.GetComponent<Platform>();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            var curPlatform = collision.gameObject.GetComponent<Platform>();
            if (curPlatform == _curPlatform)
                _curPlatform = null;
        }
    }
}