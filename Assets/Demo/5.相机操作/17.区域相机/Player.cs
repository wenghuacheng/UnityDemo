using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.AreaMove
{
    public class Player : MonoBehaviour
    {
        private Rigidbody2D rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(x,y)*3;
        }

    }
}