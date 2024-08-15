using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ObjectMove
{
    public class SimlulateGravity : MonoBehaviour
    {
        public float force = 1;

        void Start()
        {

        }

        void Update()
        {
            var p = this.transform.position;
            p.y += Physics2D.gravity.y * force * Time.deltaTime;
            this.transform.position = p;
        }
    }
}