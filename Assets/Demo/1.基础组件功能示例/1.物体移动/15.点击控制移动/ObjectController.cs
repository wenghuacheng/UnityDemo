using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ObjectMove.ClickMove
{
    public class ObjectController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Vector3 target = Vector3.zero;
        private bool hasTarget = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (hasTarget)
            {
                var direction = target - this.transform.position;
                rb.velocity = direction;
            }
        }

        /// <summary>
        /// …Ë÷√ƒø±Í
        /// </summary>
        /// <param name="position"></param>
        public void SetTarget(Vector3 position)
        {
            this.target = position;
            hasTarget = true;
        }
    }
}
