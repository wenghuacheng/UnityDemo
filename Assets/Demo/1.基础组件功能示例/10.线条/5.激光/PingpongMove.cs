using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.LineDemo
{
    public class PingpongMove : MonoBehaviour
    {
        private Vector3 originPos;
        private float distance = 6;

        private Vector3 startPos;
        private Vector3 endPos;

        private float _timeElasped = 0;

        void Start()
        {
            originPos = this.transform.position;
            startPos = this.transform.up * distance;
            endPos = this.transform.up * -distance;
        }

        void Update()
        {
            _timeElasped += Time.deltaTime * 0.5f;
            float time = Mathf.PingPong(_timeElasped, 1);

            this.transform.position = Vector3.Lerp(startPos, endPos, time);
        }
    }
}