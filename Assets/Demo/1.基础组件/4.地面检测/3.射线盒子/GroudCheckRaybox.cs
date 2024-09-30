using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroundDetection
{
    public class GroudCheckRaybox : MonoBehaviour
    {
        [SerializeField] private bool isGround;
        [SerializeField] private LayerMask layerMask;

        void Update()
        {
            //注意这边的position要是物体的脚的部分
            var result = Physics2D.OverlapBoxAll(transform.position - new Vector3(0, 1), new Vector2(0.1f, 0.1f), 0, layerMask);
            isGround = result.Length > 0;
            Debug.Log(isGround);
        }

    }
}