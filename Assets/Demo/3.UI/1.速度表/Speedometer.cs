using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class Speedometer : MonoBehaviour
    {
        //�ٶ�ָ���������Сʱ�ĽǶ�
        private int needleMinAngle = 120;
        private int needleMaxAngle = -120;

        //��ǰ�ٶ�
        private float speed = 0f;
        //����ٶ�
        private float maxSpeed = 0f;

        //ָ��
        [SerializeField] private GameObject needle;
        //�������ɿ̶ȵ�UIģ��
        [SerializeField] private GameObject markTemplate;

        private void Awake()
        {
            speed = 0f;
            maxSpeed = 200;
            //����һ��������10���ٶ�����
            Draw(10);
        }

        void Update()
        {
            speed += Time.deltaTime * 30;
            if (speed > maxSpeed) speed = maxSpeed;
            RefreshSpeed();
        }

        /// <summary>
        /// ͨ���ٶȻ�ȡָ�����ڵĽǶ�
        /// </summary>
        /// <returns></returns>
        private float GetAngle()
        {
            //����ת�ĽǶ�
            int totalAngle = needleMinAngle - needleMaxAngle;
            //��ǰ�ٶ�������ٶȵı���
            //����������ٶ�ʱ��Ƕ�ΪneedleMaxAngle���ٶ�Ϊ0ʱ��Ƕ�ΪneedleMinAngle
            float speedNormalize = speed / maxSpeed;

            //�˴���Ҫ��Ͻ�����ָ�����ת����
            return needleMinAngle - speedNormalize * totalAngle;
        }

        /// <summary>
        /// ˢ���ٶ�ָ��
        /// </summary>
        private void RefreshSpeed()
        {
            needle.transform.eulerAngles = new Vector3(0, 0, GetAngle());
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void Draw(int labelCount)
        {
            for (int i = 0; i <= labelCount; i++)
            {
                //ͨ���ٶȼ���̶ȵ�ֵ(���︴��GetAngle����)
                speed = (i / (float)labelCount) * maxSpeed;
                var angle = GetAngle();
                //����һ���̶ȿؼ�
                var markControl = Instantiate(markTemplate, transform);
                markControl.transform.eulerAngles = new Vector3(0, 0, angle);

                //�ҵ���ΪSpeedText�ı��ؼ�
                var textControl = markControl.transform.Find("SpeedText").GetComponent<Text>();
                //���ñ��̵��ٶȿ̶�
                textControl.text = speed.ToString();
                //�����ֲ������ű�����ת
                textControl.transform.eulerAngles = Vector3.zero;
                markControl.SetActive(true);
            }
            speed = 0;

            //��ָ�����������棬�����Ͳ��ᱻ�̶ȸ���
            needle.transform.SetAsLastSibling();
        }
    }
}