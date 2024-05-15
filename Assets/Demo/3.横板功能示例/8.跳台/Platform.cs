using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Platform
{
    public class Platform : MonoBehaviour
    {
        private Vector3 startPosition;
        private Vector3 endPosition;
        private float _timeElasped = 0;

        private float waitRecoverTime;//平台恢复时间

       private PlatformEffector2D platformEffector;

        void Start()
        {
            startPosition = this.transform.position - new Vector3(1, 0);
            endPosition = this.transform.position + new Vector3(1, 0);

            platformEffector = GetComponent<PlatformEffector2D>();
        }

        // Update is called once per frame
        void Update()
        {
            PingPongMove();
        }

        private void PingPongMove()
        {
            _timeElasped += Time.deltaTime;

            float time = Mathf.PingPong(_timeElasped, 1);
            this.transform.position = Vector3.Lerp(startPosition, endPosition, time);
        }

        /// <summary>
        /// 从平台上落下
        /// </summary>
        public void Drop()
        {
            //通过反转Platform Effector中offest，使其作用范围向下【即原来是上方有碰撞检测，现在改为下方有】
            platformEffector.rotationalOffset = 180;
            waitRecoverTime = 0.5f;

            Invoke("RecoverPlatform", waitRecoverTime);
        }

        /// <summary>
        /// 恢复平台
        /// </summary>
        private void RecoverPlatform()
        {
            platformEffector.rotationalOffset = 0;
            Debug.Log("平台恢复");
        }
    }
}