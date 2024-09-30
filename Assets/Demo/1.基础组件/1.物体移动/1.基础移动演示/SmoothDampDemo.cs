using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectMove
{
    public class SmoothDampDemo : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private Vector3 target;
        //����ʱ�䣬��Ӱ�������뵽��ʱ������쵽��Ŀ��ʱ����٣�ֱ��smoothTime��Żᵽ��Ŀ��λ��
        [SerializeField] float smoothTime = 3f;

        Vector3 velocity = Vector3.zero;//��¼�˶�ʱ����Ϊ��һ֡���ٶ�

        private void FixedUpdate()
        {
            //MoveTowards��ƽ����ʽ
            //�÷�ʽ���ÿ���֡���������ٶȿ�������          
            this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition, target, ref velocity, smoothTime, speed);
        }
    }
}