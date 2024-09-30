using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class LerpDemo : MonoBehaviour
    {
        [SerializeField] private Vector3 targetPosition;
        [SerializeField] private float interval = 0.1f;//ÿ�����ӵĲ�ֵ

        private float interpolationRate = 0f;//��ֵ(0-1)��ͨ����ֵ������ʼ��-������ı���
        private Vector3 startPosition;

        private void Start()
        {
            this.startPosition = this.transform.localPosition;
        }

        private void FixedUpdate()
        {
            interpolationRate += Time.fixedDeltaTime * interval;
            interpolationRate = Mathf.Min(interpolationRate, 1);

            this.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, interpolationRate);
        }
    }
}