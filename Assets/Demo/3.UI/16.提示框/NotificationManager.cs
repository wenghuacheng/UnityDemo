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

        private float offest = 10;//间距偏移

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
            // X渐变移动到指定位置
            TweeningMoveX(rt, initialPos);
            //将现有的提示框向上移动
            UpNotification(initialPos);
        }

        /// <summary>
        /// 获取屏幕尺寸
        /// 【当前只有ScaleWithScreenSize】，其他的需要扩充
        /// </summary>
        /// <returns></returns>
        private Vector2 GetScreenSize()
        {
            if (canvasScaler != null && canvasScaler.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
            {
                //Scale with Screen Size类型的屏幕尺寸从canvasScaler中获取
                return new Vector2(canvasScaler.referenceResolution.x, canvasScaler.referenceResolution.y);
            }

            //默认方式
            return new Vector2(Screen.width, Screen.height);
        }

        /// <summary>
        /// 设置初始位置
        /// </summary>
        private Vector2 SetInitialPosition(RectTransform newItem, Vector2 screenSize)
        {
            //设置初始位置【当前为左下角】        
            //这里定位点为（0，0）即左下角，所以还要缩进整体提示框的宽度
            float startPosX = -screenSize.x / 2 - newItem.rect.width;
            float startPosY = -screenSize.y / 2;
            newItem.localPosition = new Vector3(startPosX, startPosY, 0);
            return newItem.localPosition;
        }

        /// <summary>
        /// 渐变移动X
        /// </summary>
        private void TweeningMoveX(RectTransform rect, Vector2 initialPos)
        {
            // 移动到指定位置
            var targetX = initialPos.x + rect.rect.width + offest;
            rect.DOLocalMoveX(targetX, 0.5f);
        }

        /// <summary>
        /// 将提示框向上移动
        /// </summary>
        private void UpNotification(Vector2 initialPos)
        {
            var childNotifications = notificationContainer.GetComponentsInChildren<NotificationItemVisual>();
            for (int i = childNotifications.Length - 1; i >= 0; i--)
            {
                var rect = childNotifications[i].GetComponent<RectTransform>();
                //需要考虑显示过快时两个显示框重叠的情况
                var targetOffestY = (rect.rect.height + offest) * (childNotifications.Length - i);
                var targetY = initialPos.y + targetOffestY;
                rect.DOLocalMoveY(targetY, 0.5f);
            }
        }


        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 50), "生成"))
            {
                ShowNotification("111111");
            }
        }
    }
}