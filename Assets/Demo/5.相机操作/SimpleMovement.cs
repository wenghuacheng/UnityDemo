using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera
{
    public class SimpleMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 3;

        void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            this.transform.Translate(new Vector3(x, y) * Time.deltaTime * speed);
        }
    }
}