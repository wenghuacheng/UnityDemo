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

        private float waitRecoverTime;//ƽ̨�ָ�ʱ��

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
        /// ��ƽ̨������
        /// </summary>
        public void Drop()
        {
            //ͨ����תPlatform Effector��offest��ʹ�����÷�Χ���¡���ԭ�����Ϸ�����ײ��⣬���ڸ�Ϊ�·��С�
            platformEffector.rotationalOffset = 180;
            waitRecoverTime = 0.5f;

            Invoke("RecoverPlatform", waitRecoverTime);
        }

        /// <summary>
        /// �ָ�ƽ̨
        /// </summary>
        private void RecoverPlatform()
        {
            platformEffector.rotationalOffset = 0;
            Debug.Log("ƽ̨�ָ�");
        }
    }
}