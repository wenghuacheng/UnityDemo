using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Platform
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            var x = Input.GetAxis("Horizontal");
            int y = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                y = 10;
            }

            rb.velocity = new Vector2(x, y) * 5;

          
        }
    }
}