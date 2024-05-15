using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    /// <summary>
    /// Ñ­»·ÒÆ¶¯
    /// </summary>
    public class PingpongMoveDemo : MonoBehaviour
    {
        [SerializeField] private Vector3 startPosition;
        [SerializeField] private Vector3 endPosition;

        private float _timeElasped = 0;

        void Update()
        {
            _timeElasped += Time.deltaTime;

            float time = Mathf.PingPong(_timeElasped, 1);
            this.transform.position = Vector3.Lerp(startPosition, endPosition, time);
        }
    }
}