using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroundDetection
{
    public class GroudCheckRay : MonoBehaviour
    {
        [SerializeField] private bool isGround;
        [SerializeField] private LayerMask layerMask;

        void Update()
        {
            var result = Physics2D.RaycastAll(transform.position, Vector2.down, 1.1f, layerMask);
            isGround = result.Length > 0;
            Debug.Log(isGround);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(this.transform.position, Vector2.down * 1.1f);
        }
    }
}
