using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.PostProcessing
{
    public class BoxVolumeMove : MonoBehaviour
    {
        [SerializeField] private BoxCollider box;
        private float _timeElasped = 0;

        void Update()
        {
            _timeElasped += Time.deltaTime;
            float time = Mathf.PingPong(_timeElasped, 1);
            var offest= Vector3.Lerp(new Vector3(-2, 0), new Vector3(2, 0), time);
            this.box.center = offest;
        }
    }
}
