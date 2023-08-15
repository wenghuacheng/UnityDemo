using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class ObjMovePosition : MonoBehaviour
    {
        private float speed = 5;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            //针对非dynamic类型的刚体移动，因为不会收到AddForce的影响
            var interval = Vector3.right * speed * Time.deltaTime;
            this.transform.GetComponent<Rigidbody2D>().MovePosition(this.transform.position + interval);
        }
    }
}
