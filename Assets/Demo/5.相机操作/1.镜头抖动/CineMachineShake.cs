using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CineMachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private CinemachineBasicMultiChannelPerlin _multiChannelPerlin;

    //当前震动持续时间
    private float _shakeTime;
    //每次震动总时间
    private float _shakeTimeTotal;
    //震动强度
    private float _shakeIntensity;


    private void Awake()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        //这里获取相机的组件
        _multiChannelPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        //测试数值
        _shakeIntensity = 5;
        _shakeTimeTotal = 0.5f;
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
            //注意这里是递减
            _multiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0, _shakeIntensity, _shakeTime / _shakeTimeTotal);
        }
        //else
        //{
        //    _multiChannelPerlin.m_AmplitudeGain = 0;
        //}
    }

    /// <summary>
    /// 开始镜头震动
    /// </summary>
    private void ShakeCamera()
    {
        _shakeTime = _shakeTimeTotal;
        _multiChannelPerlin.m_AmplitudeGain = _shakeIntensity;
    }
}
