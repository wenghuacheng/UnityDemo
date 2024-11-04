using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.Lights
{
    public class Pingpong : MonoBehaviour
    {
        private float _currentRotation;
        private float _rotationSpeed = 10;
        private float _maxRotation = 45;


        void Update()
        {
            RotateHead();
        }

        private void RotateHead()
        {
            _currentRotation += Time.deltaTime * _rotationSpeed;
            float z = Mathf.PingPong(_currentRotation, _maxRotation);
            this.transform.localRotation = Quaternion.Euler(0, 0, z);
        }
    }
}