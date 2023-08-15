using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class ObjTranslate : MonoBehaviour
    {
        private float speed = 1;

        private void Update()
        {
            //会向着自身的上方向运行，已经进行了旋转不需要再次旋转了
            this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

}

