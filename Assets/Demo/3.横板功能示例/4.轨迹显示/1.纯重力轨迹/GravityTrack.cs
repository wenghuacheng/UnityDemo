using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Track
{
    /// <summary>
    /// 重力轨迹模拟
    /// </summary>
    public class GravityTrack : MonoBehaviour
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
            //自由落体运动的位移公式，位移d与时间t的关系可以表示为d = 0.5 * g * t^2

            float dotSpace = 0.1f;//点间距，用于控制时间参数
            for (int i = 0; i < dotCount; i++)
            {
                var dot = dotList[i];
                var t = i * dotSpace;//公式中t为时间，这里代表间距

                //这里模拟收到重力影响，基于时间收到的距离
                dot.transform.position = .5f * (Physics2D.gravity) * (t * t);
            }
        }
    }
}
