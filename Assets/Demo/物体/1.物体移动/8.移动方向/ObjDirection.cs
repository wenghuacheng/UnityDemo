using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class ObjDirection : MonoBehaviour
    {
        [SerializeField] private Transform target;

        void Update()
        {
            //通过与目标点相减计算方向
            var direction = (target.position - this.transform.position).normalized;
            Debug.Log(direction);
            this.transform.Translate(direction * 2 * Time.deltaTime);
        }
    }
}
