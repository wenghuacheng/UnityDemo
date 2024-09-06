using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Misc
{
    public class SelectManager : MonoBehaviour
    {
        public float MinSize = 10f;
        public float MinTime = 0.1f;
        public GameObject[] allCharacters;//��ѡ������

        private float selectTimer = 0f;
        public Vector2 selectingStart;
        private RectTransform selectingBoxRect;//ѡ��

        private void Awake()
        {
            selectingBoxRect = GetComponent<RectTransform>();
        }


        private void Update()
        {
            //��갴��
            if (Input.GetMouseButtonDown(0))
            {
                selectingStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                selectingBoxRect.anchoredPosition = selectingStart;//����ѡ��ê��λ��
            }

            //���̧��
            if (Input.GetMouseButtonUp(0))
            {
                if (selectTimer <= MinTime)
                {
                    ClickObject();
                }
                //����ѡ��״̬
                selectTimer = 0f;
                selectingBoxRect.sizeDelta = Vector2.zero;
            }

            //����ƶ�
            if (Input.GetMouseButton(0))
            {
                selectTimer += Time.deltaTime;
                if (selectTimer > MinTime)
                {
                    //��ѡ��ʾ��Ӧ�������̧��ʱ�ж�
                    SelectingObjects();
                }
            }
        }

        private void ClickObject()
        {

        }

        /// <summary>
        /// ѡ��
        /// ע��anchor��ʽ
        /// </summary>
        private void SelectingObjects()
        {
            //ѡ��ߴ�
            Vector2 sizeDelta = Vector2.zero;
            sizeDelta.x = MathF.Abs(selectingStart.x - Input.mousePosition.x);
            sizeDelta.y = MathF.Abs(selectingStart.y - Input.mousePosition.y);

            Rect rect = Rect.zero;
            Vector2 piot = Vector2.zero;//ê����

            if (selectingStart.x - Input.mousePosition.x < 0)
            {
                //�ƶ�����Ϊ��ʼ�����
                rect.x = selectingStart.x;
            }
            else
            {
                //�ƶ�����Ϊ��ʼ���Ҳ�
                piot.x = 1;
                rect.x = selectingStart.x - selectingBoxRect.sizeDelta.x;
            }


            if (selectingStart.y - Input.mousePosition.y > 0)
            {
                //�ƶ�����Ϊ��ʼ���Ϸ�
                piot.y = 1;
                rect.y = selectingStart.y - selectingBoxRect.sizeDelta.y;
            }
            else
            {
                //�ƶ�����Ϊ��ʼ���·�
                rect.y = selectingStart.y;
            }

            selectingBoxRect.sizeDelta = sizeDelta;

            if (selectingBoxRect.pivot != piot)
                selectingBoxRect.pivot = piot;


            rect.width = sizeDelta.x;
            rect.height = sizeDelta.y;

            if (sizeDelta.x > MinSize && sizeDelta.y > MinSize)
            {
                //todo��ѡ�����
                CheckForSelectedCharacters();
            }
        }


        /// <summary>
        /// ��������Ƿ�ѡ��
        /// </summary>
        private void CheckForSelectedCharacters()
        {
            foreach (GameObject character in allCharacters)
            {
                var screenPos = Camera.main.WorldToScreenPoint(character.transform.position);
                if (RectTransformUtility.RectangleContainsScreenPoint(selectingBoxRect, screenPos))
                {
                    Debug.Log("ѡ��");
                }
            }
        }
    }
}