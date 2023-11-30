using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera
{
    public class CameraCameraLimitBorder : MonoBehaviour
    {
        [SerializeField] private Transform target;

        Vector3 bounds = Vector3.zero;
        private void Start()
        {

        }

        void Update()
        {
            Vector3 velocity = Vector3.zero;

            //设置边界区域【相机只能在(-4,4)的范围内】
            Vector3 bounds = new Vector3(
              Mathf.Clamp(target.position.x, -4f, 4f),
               Mathf.Clamp(target.position.y, -4f, 4f),
               this.transform.position.z
              );

            this.transform.position = Vector3.SmoothDamp(this.transform.position
                , bounds, ref velocity, 0.06f);
        }
    }
}