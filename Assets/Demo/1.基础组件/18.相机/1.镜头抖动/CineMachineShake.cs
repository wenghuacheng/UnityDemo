using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.Shakes
{
    public class CineMachineShake : MonoBehaviour
    {
        //ÿ������ʱ��
        [SerializeField] private float _shakeTimeTotal = 0.5f;
        //��ǿ��
        [SerializeField] private float _shakeIntensity = 5f;

        //��ǰ�𶯳���ʱ��
        private float _shakeTime;
        private CinemachineVirtualCamera _cinemachineVirtualCamera;
        private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

        private void Awake()
        {
            _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            //�����ȡ��������
            _multiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                ShakeCamera();
            }

            if (_shakeTime > 0)
            {
                _shakeTime -= Time.deltaTime;
                //ע�������ǵݼ�
                _multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0, _shakeIntensity, _shakeTime / _shakeTimeTotal);
            }
        }

        /// <summary>
        /// ��ʼ��ͷ��
        /// </summary>
        private void ShakeCamera()
        {
            _shakeTime = _shakeTimeTotal;
            _multiChannelPerlin.m_AmplitudeGain = _shakeIntensity;
        }
    }
}