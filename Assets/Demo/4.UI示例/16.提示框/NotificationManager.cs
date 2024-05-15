using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI.Notifications
{
    public class NotificationManager : MonoBehaviour
    {
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private Transform notificationContainer;
        [SerializeField] private CanvasScaler canvasScaler;

        private float offest = 10;//���ƫ��

        private void Awake()
        {
            Debug.Log(Screen.width);
        }

        public void ShowNotification(string message)
        {
            GameObject newNotification = Instantiate(notificationPrefab, notificationContainer);
            newNotification.GetComponentInChildren<TextMeshProUGUI>().text = message;

            RectTransform rt = newNotification.GetComponent<RectTransform>();

            var screenSize = GetScreenSize();
            var initialPos = SetInitialPosition(rt, screenSize);
            // X�����ƶ���ָ��λ��
            TweeningMoveX(rt, initialPos);
            //�����е���ʾ�������ƶ�
            UpNotification(initialPos);
        }

        /// <summary>
        /// ��ȡ��Ļ�ߴ�
        /// ����ǰֻ��ScaleWithScreenSize������������Ҫ����
        /// </summary>
        /// <returns></returns>
        private Vector2 GetScreenSize()
        {
            if (canvasScaler != null && canvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
            {
                //Scale with Screen Size���͵���Ļ�ߴ��canvasScaler�л�ȡ
                return new Vector2(canvasScaler.referenceResolution.x, canvasScaler.referenceResolution.y);
            }

            //Ĭ�Ϸ�ʽ
            return new Vector2(Screen.width, Screen.height);
        }

        /// <summary>
        /// ���ó�ʼλ��
        /// </summary>
        private Vector2 SetInitialPosition(RectTransform newItem, Vector2 screenSize)
        {
            //���ó�ʼλ�á���ǰΪ���½ǡ�        
            //���ﶨλ��Ϊ��0��0�������½ǣ����Ի�Ҫ����������ʾ��Ŀ��
            float startPosX = -screenSize.x / 2 - newItem.rect.width;
            float startPosY = -screenSize.y / 2;
            newItem.localPosition = new Vector3(startPosX, startPosY, 0);
            return newItem.localPosition;
        }

        /// <summary>
        /// �����ƶ�X
        /// </summary>
        private void TweeningMoveX(RectTransform rect, Vector2 initialPos)
        {
            // �ƶ���ָ��λ��
            var targetX = initialPos.x + rect.rect.width + offest;
            rect.DOLocalMoveX(targetX, 0.5f);
        }

        /// <summary>
        /// ����ʾ�������ƶ�
        /// </summary>
        private void UpNotification(Vector2 initialPos)
        {
            var childNotifications = notificationContainer.GetComponentsInChildren<NotificationItemVisual>();
            for (int i = childNotifications.Length - 1; i >= 0; i--)
            {
                var rect = childNotifications[i].GetComponent<RectTransform>();
                //��Ҫ������ʾ����ʱ������ʾ���ص������
                var targetOffestY = (rect.rect.height + offest) * (childNotifications.Length - i);
                var targetY = initialPos.y + targetOffestY;
                rect.DOLocalMoveY(targetY, 0.5f);
            }
        }


        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), "����"))
            {
                ShowNotification("111111");
            }
        }
    }
}