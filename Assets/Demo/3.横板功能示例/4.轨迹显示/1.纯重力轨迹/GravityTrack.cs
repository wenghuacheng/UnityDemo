using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Track
{
    /// <summary>
    /// �����켣ģ��
    /// </summary>
    public class GravityTrack : MonoBehaviour
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
            //���������˶���λ�ƹ�ʽ��λ��d��ʱ��t�Ĺ�ϵ���Ա�ʾΪd = 0.5 * g * t^2

            float dotSpace = 0.1f;//���࣬���ڿ���ʱ�����
            for (int i = 0; i < dotCount; i++)
            {
                var dot = dotList[i];
                var t = i * dotSpace;//��ʽ��tΪʱ�䣬���������

                //����ģ���յ�����Ӱ�죬����ʱ���յ��ľ���
                dot.transform.position = .5f * (Physics2D.gravity) * (t * t);
            }
        }
    }
}
