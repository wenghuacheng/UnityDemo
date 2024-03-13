using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.AreaMove
{
    public class CameraArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("´¥·¢");
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log("111");
        }
    }
}