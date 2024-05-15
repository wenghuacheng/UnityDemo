using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.BrickBreaker
{
    /// <summary>
    /// С��
    /// </summary>
    public class Ball : MonoBehaviour
    {
        private float speed = 20;//С���ƶ��ٶ�

        //�ƶ�����
        private Vector2 direction;

        void Start()
        {
            direction = new Vector2(0.5f, -0.5f);//��ʼ����
        }

        void Update()
        {
            Forward();
        }

        /// <summary>
        /// ǰ��
        /// </summary>
        private void Forward()
        {
            this.transform.Translate(direction * Time.deltaTime * speed);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            //�Ӵ��㷨�߷���
            //�ر�ע�⣬���岻�ܽ�����ת�������ȡ�ĽӴ��淨�߻�������
            var normal = collision.contacts[0].normal;
            //���㷴������
            var newDirection = Vector2.Reflect(direction, normal);
            direction = newDirection;
            Debug.Log("�������꣺" + normal + "��������:" + direction);
        }
    }
}