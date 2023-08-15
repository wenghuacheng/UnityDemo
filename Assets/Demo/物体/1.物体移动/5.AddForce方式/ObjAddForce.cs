using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class ObjAddForce : MonoBehaviour
    {
        void Start()
        {
            //在有阻尼的情况下会慢慢停下来
            //如果需要持续移动则需要在Update中持续加力
            this.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 200);
        }

    }
}
