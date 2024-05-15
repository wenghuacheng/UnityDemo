using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    /// <summary>
    /// ����Ŀ����ƶ�
    /// </summary>
    public class MoveToTargetDemo : MonoBehaviour
    {
        [SerializeField] private Transform target;

        void Update()
        {
            //ͨ����Ŀ���������㷽��
            var direction = (target.position - this.transform.position).normalized;
            this.transform.Translate(direction * Time.deltaTime);
        }
    }
}
