using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectMove
{
    public class MovePositionDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private Vector3 target;

        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (this.transform.localPosition.x > target.x)
                return;

            //针对position而不是localPosition
            var newPos = this.transform.position + Vector3.right * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
    }
}
