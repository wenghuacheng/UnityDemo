using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class MoveTowardsDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private Vector3 target;

        void FixedUpdate()
        {
            this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, target, speed * Time.fixedDeltaTime);
        }
    }
}