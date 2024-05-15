using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI.QTE
{
    public class HitArea : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 50;//ָ����ת�ٶ�
        [SerializeField] private RectTransform needleRect;//ָ��
        [SerializeField] private RectTransform gArea;//��ɫ����
        [SerializeField] private RectTransform yLeftArea;//��ɫ����
        [SerializeField] private RectTransform yRightArea;//��ɫ����

        private float angle = 0;//��ǰ�Ƕ�
        private int rotateDirection = 1;//��ת����

        private float startAngle;//������ʼλ��
        private float yellowLeftAreaAngle;//����ɫ����Ƕ�
        private float greenAreaAngle;//�м���ɫ����Ƕ�
        private float yellowRightAreaAngle;//�в��ɫ����Ƕ�

        private bool isRunning = false;

        void Start()
        {
            needleRect.rotation = Quaternion.identity;
            CreateArea();
            isRunning = true;
        }

        void Update()
        {
            AutoRotateNeedle();
            CheckArea();
        }

        /// <summary>
        /// �Զ���תָ��
        /// </summary>
        private void AutoRotateNeedle()
        {
            if (!isRunning) return;

            if (angle <= -90)
                rotateDirection = 1;
            else if (angle >= 90)
                rotateDirection = -1;

            angle += rotationSpeed * Time.deltaTime * rotateDirection;
            needleRect.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        /// <summary>
        /// ȷ��ָ������
        /// </summary>
        private void CheckArea()
        {
            if (isRunning && Input.GetMouseButtonDown(0))
            {
                isRunning = false;

                //�����0��ʼ����ת����Ϊ��������
                //todo:����������ת���Ǹ����Ƕȣ����濼���Ż�
                var lAreaStartAngle = -startAngle;
                var lAreaEndAngle = -(startAngle + yellowLeftAreaAngle);
                var gAreaEndAngle = -(startAngle + yellowLeftAreaAngle + greenAreaAngle);
                var rAreaEndAngle = -(startAngle + yellowLeftAreaAngle + greenAreaAngle + yellowRightAreaAngle);

                var checkAngle = angle - 90;//ָ��0���м䣬������ҪУ׼

                Debug.Log(checkAngle);
                Debug.Log($"{lAreaStartAngle}-{lAreaEndAngle}-{gAreaEndAngle}-{rAreaEndAngle}");

                if (checkAngle <= lAreaStartAngle && checkAngle > lAreaEndAngle)
                {
                    Debug.Log("��������ɫ����");
                }
                else if (checkAngle <= lAreaEndAngle && checkAngle > gAreaEndAngle)
                {
                    Debug.Log("������ɫ����");
                }
                else if (checkAngle <= gAreaEndAngle && checkAngle > rAreaEndAngle)
                {
                    Debug.Log("�����Ҳ��ɫ����");
                }
                else
                {
                    Debug.Log("δ����");
                }
            }
            else if(!isRunning && Input.GetMouseButtonDown(0))
            {
                isRunning = true;//���¿�ʼ
            }
        }

        #region ��������

        private void CreateArea()
        {
            //������������ĽǶ�
            yellowLeftAreaAngle = RamdomYAngle();
            yellowRightAreaAngle = RamdomYAngle();
            greenAreaAngle = RamdomGAngle();

            var allAngle = yellowLeftAreaAngle + yellowRightAreaAngle + greenAreaAngle;

            //������ʾ����Ĵ�С�����ڽǶ���ʾ�Ǹ����֡�
            gArea.GetComponent<Image>().fillAmount = greenAreaAngle / 360f;
            yLeftArea.GetComponent<Image>().fillAmount = yellowLeftAreaAngle / 360f;
            yRightArea.GetComponent<Image>().fillAmount = yellowRightAreaAngle / 360f;

            startAngle = Random.Range(0, 180 - allAngle);
            //��ת��ص�����
            yLeftArea.rotation = Quaternion.Euler(new Vector3(0, 0, -startAngle));
            gArea.rotation = Quaternion.Euler(new Vector3(0, 0, -startAngle - yellowLeftAreaAngle));
            yRightArea.rotation = Quaternion.Euler(new Vector3(0, 0, -startAngle - yellowLeftAreaAngle - greenAreaAngle));
        }

        /// <summary>
        /// ��ȡ��ɫ��������ɽǶ�
        /// </summary>
        private float RamdomGAngle()
        {
            return Random.Range(0.02f, 0.03f) * 360;
        }

        /// <summary>
        /// ��ȡ��ɫ��������ɽǶ�
        /// </summary>
        private float RamdomYAngle()
        {
            return Random.Range(0.05f, 0.1f) * 360;
        }
        #endregion
    }
}