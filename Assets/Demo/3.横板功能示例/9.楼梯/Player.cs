using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Ladder
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;

        [SerializeField] private LayerMask whatIsLadder;
        [SerializeField] private Transform checkLadderPosition;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Movement();
            Climb();
        }

        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(x * 5, rb.velocity.y);
        }

        /// <summary>
        /// 爬梯
        /// </summary>
        private void Climb()
        {
            var isOnLadder = IsOnLadder();
            Debug.Log(isOnLadder);
            if (isOnLadder)
            {
                rb.gravityScale = 0;
                var y = Input.GetAxis("Vertical");
                rb.velocity = new Vector2(rb.velocity.x, y * 3);
            }
            else
            {
                rb.gravityScale = 1;
            }
        }

        /// <summary>
        /// 是否角色在梯子上
        /// </summary>
        private bool IsOnLadder()
        {
            var hit2D = Physics2D.Raycast(checkLadderPosition.position, Vector2.up, 1f, whatIsLadder);
            return hit2D.transform != null;
        }
    }
}
