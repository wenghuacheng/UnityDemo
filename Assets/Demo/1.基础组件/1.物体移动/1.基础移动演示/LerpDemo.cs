using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class LerpDemo : MonoBehaviour
    {
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private float interval = 0.1f;//每秒增加的插值

        private float interpolationRate = 0f;//插值(0-1)。通过该值控制起始点-结束点的比例
        private Vector3 startPosition;

        private void Start()
        {
            this.startPosition = this.transform.localPosition;
        }

        private void FixedUpdate()
        {
            interpolationRate += Time.fixedDeltaTime * interval;
            interpolationRate = Mathf.Min(interpolationRate, 1);

            this.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, interpolationRate);
        }
    }
}