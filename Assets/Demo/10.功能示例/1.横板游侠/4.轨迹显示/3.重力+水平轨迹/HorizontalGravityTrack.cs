using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Track
{
    public class HorizontalGravityTrack : MonoBehaviour
    {
        [SerializeField] private GameObject dotPerfab;
        [SerializeField] private GameObject bulletPerfab;

        //�켣������
        private int dotCount = 20;
        //�켣����ʾ�б�
        private GameObject[] dotList;
        //��
        private Vector2 force = new Vector2(3, 5);

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
            //���������˶���λ�ƹ�ʽ��λ��d��ʱ��t�Ĺ�ϵ���Ա�ʾΪd = 0.5 * g * t^2

            //���࣬���ڿ���ʱ�����
            float dotSpace = 0.1f;
            //����
            var direction = AimDirection();

            for (int i = 0; i < dotCount; i++)
            {
                var dot = dotList[i];
                var t = i * dotSpace;//��ʽ��tΪʱ�䣬���������

                //����ģ���յ�����Ӱ�죬����ʱ���յ��ľ���
                dot.transform.position =
                    (Vector2)this.transform.position//��ʼ��
                    + new Vector2(direction.x * force.x, direction.y * force.y) * t//ˮƽ
                    + .5f * (Physics2D.gravity) * (t * t);//��ֱ
            }

            Fire();
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


        /// <summary>
        /// ����ģ������
        /// </summary>
        private void Fire()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var dir = AimDirection();

            var obj = Instantiate(bulletPerfab, this.transform.position, Quaternion.identity, transform);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * force.x, dir.y * force.y);//��Ҫ��켣����һ��
        }
    }
}