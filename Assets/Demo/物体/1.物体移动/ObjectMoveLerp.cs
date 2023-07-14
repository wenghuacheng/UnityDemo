using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveLerp : MonoBehaviour
{
    [SerializeField]private Transform target;
    private Vector3 targetPosition;
    private Vector3 startPosition;

    private float lerpDuration = 4;
    private float _timeElasped = 0;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = target.transform.position;
    }

    void Update()
    {
        _timeElasped += Time.deltaTime;
    
        if (_timeElasped < lerpDuration)
        {
            //通过已运行的时间与预计耗时的比例计算当前位置
            //this.transform.position = Vector3.Lerp(startPosition, targetPosition, _timeElasped / lerpDuration);

            //使用缓入方式【还有缓出和缓入缓出甚至弹跳方式】
            this.transform.position = Vector3.Lerp(startPosition, targetPosition, EaseIn(_timeElasped / lerpDuration));
            
        }
        else
        {
            //Lerp只会接近目标而不会达到，所以需要手动设置
            this.transform.position = targetPosition;
        }
    }

    /// <summary>
    /// 缓入函数
    /// </summary>
    /// <param name="k"></param>
    /// <returns></returns>
    private float EaseIn(float k)
    {
        return k * k * k;
    }

    private float EaseOut(float k)
    {
        return 1f + ((k -= 1f) * k * k);
    }
}
