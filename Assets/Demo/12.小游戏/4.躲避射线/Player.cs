using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.HideRaycast
{
    public class Player : MonoBehaviour
    {
        private float moveSpeed = 10;
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            Movement();
        }

        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            rb.velocity = new Vector2(x, y) * moveSpeed;
        }
    }
}