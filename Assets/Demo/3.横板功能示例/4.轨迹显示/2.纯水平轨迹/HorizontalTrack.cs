using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Track
{
    /// <summary>
    /// 水平力的轨迹
    /// </summary>
    public class HorizontalTrack : MonoBehaviour
    {
        [SerializeField] private GameObject dotPerfab;

        //轨迹点数量
        private int dotCount = 10;
        //轨迹点显示列表
        private GameObject[] dotList;

        private void Awake()
        {
            dotList = new GameObject[dotCount];

            for (int i = 0; i < dotCount; i++)
            {
                var obj = Instantiate(dotPerfab, Vector3.zero, Quaternion.identity, transform);
                dotList[i] = obj;
                //可以通过设置active来隐藏/显示轨迹点
                obj.SetActive(true);
            }
        }


        void Update()
        {
            //点间距，用于控制时间参数
            float dotSpace = 0.1f;
            //方向
            var direction = AimDirection();
            //力
            //var force = new Vector2(1, 0);//只有x时只会在水平上进行移动
            var force = new Vector2(1, 1);//会跟随鼠标移动，完全指向目标
            //var force = new Vector2(0, 1);//只有y时只会在垂直上进行移动
            //var force = new Vector2(1, 10);//会影响y轴的间距且会有一定的偏移，不会完全指向鼠标

            for (int i = 0; i < dotCount; i++)
            {
                var dot = dotList[i];
                var t = i * dotSpace;//公式中t为时间，这里代表间距

                //这里模拟收到重力影响，基于时间收到的距离
                dot.transform.position = this.transform.position +
                    new Vector3(direction.x * force.x, direction.y * force.y) * t;
            }
        }

        /// <summary>
        /// 获取当前物体到鼠标的方向
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
