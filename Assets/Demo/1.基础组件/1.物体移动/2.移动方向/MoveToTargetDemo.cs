using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    /// <summary>
    /// 基于目标的移动
    /// </summary>
    public class MoveToTargetDemo : MonoBehaviour
    {
        [SerializeField] private Transform target;

        void Update()
        {
            //通过与目标点相减计算方向
            var direction = (target.position - this.transform.position).normalized;
            this.transform.Translate(direction * Time.deltaTime);
        }
    }
}
