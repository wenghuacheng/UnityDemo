using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class ObjMoveToward : MonoBehaviour
    {
        private float speed = 3;
        private Vector3 target = new Vector3(5, 0, 0);

        void Update()
        {
            //移动方式为开始-结束点
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
        }
    }
}