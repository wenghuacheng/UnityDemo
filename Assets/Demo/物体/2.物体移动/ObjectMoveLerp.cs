using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveLerp : MonoBehaviour
{
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private float lerpDuration = 4;
    private float _timeElasped = 0;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(5, 0, 0);
    }

    void Update()
    {
        _timeElasped += Time.deltaTime;
        if (_timeElasped < lerpDuration)
        {
            //通过已运行的时间与预计耗时的比例计算当前位置
            this.transform.position = Vector3.Lerp(startPosition, targetPosition, _timeElasped / lerpDuration);
        }
        else
        {
            //Lerp只会接近目标而不会达到，所以需要手动设置
            this.transform.position = targetPosition;
        }
    }
}
