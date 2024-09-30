using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.EdgeMove
{
    /// <summary>
    /// 屏幕边缘移动相机
    /// </summary>
    public class ScreenEdgeMove : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        void Update()
        {
            var pos = mainCamera.ScreenToViewportPoint(Input.mousePosition);

            Vector2 direction = Vector2.zero;

            #region 水平方向移动
            if (pos.x <= 0.1f)
            {
                direction.x = -1;
            }
            else if (pos.x >= 0.9f)
            {
                direction.x = 1;
            }
            #endregion

            #region 垂直方向移动
            if (pos.y <= 0.1f)
            {
                direction.y = -1;
            }
            else if (pos.y >= 0.9f)
            {
                direction.y = 1;
            }
            #endregion

            mainCamera.transform.Translate(direction * Time.deltaTime);
        }
    }
}