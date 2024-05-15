using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.Ability
{
    /// <summary>
    /// ������
    /// </summary>
    public class RPGPlayerGroundCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask layerMask;
        //���ڵ��Ծ���
        [SerializeField] private float distance = 0.54f;

        /// <summary>
        /// �Ƿ��ڵ���
        /// </summary>
        public bool IsOnGround { get; private set; }

        void Update()
        {
            var hit = Physics2D.Raycast(this.transform.position, Vector2.down, distance, layerMask);
            IsOnGround = hit.collider != null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y - distance));
        }
    }
}