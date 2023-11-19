using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.MiniMap
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            Vector3 offest = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
                offest = new Vector3(0, 1);
            if (Input.GetKey(KeyCode.S))
                offest = new Vector3(0, -1);
            if (Input.GetKey(KeyCode.A))
                offest = new Vector3(-1, 0);
            if (Input.GetKey(KeyCode.D))
                offest = new Vector3(1, 0);

            this.transform.position += offest * Time.deltaTime * 5;
        }
    }
}