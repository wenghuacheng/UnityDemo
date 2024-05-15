using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class TranslateDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private Vector3 target;

        private void FixedUpdate()
        {
            if (this.transform.localPosition.x > target.x)
                return;

            this.transform.Translate(Vector3.right * speed * Time.fixedDeltaTime, Space.Self);
        }
    }
}