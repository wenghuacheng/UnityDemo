using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.VFX.Trails
{
    public class ObjRotation : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            this.transform.RotateAround(Vector3.zero, Vector3.up, 30 * Time.deltaTime);
        }
    }
}