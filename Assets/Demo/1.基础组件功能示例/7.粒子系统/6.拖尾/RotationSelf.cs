using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PS
{
    public class RotationSelf : MonoBehaviour
    {
        void Update()
        {
            this.transform.RotateAround(Vector2.zero, Vector3.forward, 40 * Time.deltaTime);
        }
    }
}