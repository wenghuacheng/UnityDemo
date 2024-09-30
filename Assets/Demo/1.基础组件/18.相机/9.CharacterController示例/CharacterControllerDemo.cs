using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.ThreeD
{
    public class CharacterControllerDemo : MonoBehaviour
    {
        private CharacterController controller;

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            var z = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");

            controller.Move(new Vector3(z, 0, y) * Time.deltaTime);
        }
    }
}