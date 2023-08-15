using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class ObjPingpong : MonoBehaviour
    {
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 endPosition;

        private float _timeElasped = 0;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _timeElasped += Time.deltaTime;

            float time = Mathf.PingPong(_timeElasped, 1);
            Debug.Log(time);
            this.transform.position = Vector3.Lerp(startPosition, endPosition, time);
        }
    }
}