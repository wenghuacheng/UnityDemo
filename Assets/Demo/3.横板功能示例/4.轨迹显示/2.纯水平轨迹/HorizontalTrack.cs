using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Track
{
    /// <summary>
    /// ˮƽ���Ĺ켣
    /// </summary>
    public class HorizontalTrack : MonoBehaviour
    {
        [SerializeField] private GameObject dotPerfab;

        //�켣������
        private int dotCount = 10;
        //�켣����ʾ�б�
        private GameObject[] dotList;

        private void Awake()
        {
            dotList = new GameObject[dotCount];

            for (int i = 0; i < dotCount; i++)
            {
                var obj = Instantiate(dotPerfab, Vector3.zero, Quaternion.identity, transform);
                dotList[i] = obj;
                //����ͨ������active������/��ʾ�켣��
                obj.SetActive(true);
            }
        }


        void Update()
        {
            //���࣬���ڿ���ʱ�����
            float dotSpace = 0.1f;
            //����
            var direction = AimDirection();
            //��
            //var force = new Vector2(1, 0);//ֻ��xʱֻ����ˮƽ�Ͻ����ƶ�
            var force = new Vector2(1, 1);//���������ƶ�����ȫָ��Ŀ��
            //var force = new Vector2(0, 1);//ֻ��yʱֻ���ڴ�ֱ�Ͻ����ƶ�
            //var force = new Vector2(1, 10);//��Ӱ��y��ļ���һ���һ����ƫ�ƣ�������ȫָ�����

            for (int i = 0; i < dotCount; i++)
            {
                var dot = dotList[i];
                var t = i * dotSpace;//��ʽ��tΪʱ�䣬���������

                //����ģ���յ�����Ӱ�죬����ʱ���յ��ľ���
                dot.transform.position = this.transform.position +
                    new Vector3(direction.x * force.x, direction.y * force.y) * t;
            }
        }

        /// <summary>
        /// ��ȡ��ǰ���嵽���ķ���
        /// </summary>
        /// <returns></returns>
        private Vector2 AimDirection()
        {
            Vector2 playerPosition = this.transform.position;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePosition - playerPosition;
            return direction.normalized;
        }
    }
}
