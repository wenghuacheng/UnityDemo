using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.Rb.CollisonDemo
{
    public class MoveFromRightToLeft : MonoBehaviour
    {
        private Rigidbody2D rb;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            this.transform.Translate(Vector2.left * Time.deltaTime);
        }
    }
}