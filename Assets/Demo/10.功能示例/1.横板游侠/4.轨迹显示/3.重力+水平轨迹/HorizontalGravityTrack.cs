using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo.Track
{
    public class HorizontalGravityTrack : MonoBehaviour
    {
        [SerializeField] private GameObject dotPerfab;
        [SerializeField] private GameObject bulletPerfab;

        //轨迹点数量
        private int dotCount = 20;
        //轨迹点显示列表
        private GameObject[] dotList;
        //力
        private Vector2 force = new Vector2(3, 5);

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

            //点间距，用于控制时间参数
            float dotSpace = 0.1f;
            //方向
            var direction = AimDirection();

            for (int i = 0; i < dotCount; i++)
            {
                var dot = dotList[i];
                var t = i * dotSpace;//公式中t为时间，这里代表间距

                //这里模拟收到重力影响，基于时间收到的距离
                dot.transform.position =
                    (Vector2)this.transform.position//起始点
                    + new Vector2(direction.x * force.x, direction.y * force.y) * t//水平
                    + .5f * (Physics2D.gravity) * (t * t);//垂直
            }

            Fire();
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


        /// <summary>
        /// 发送模拟物体
        /// </summary>
        private void Fire()
        {
            if (!Input.GetMouseButtonDown(0)) return;

            var dir = AimDirection();

            var obj = Instantiate(bulletPerfab, this.transform.position, Quaternion.identity, transform);
            obj.GetComponent<Rigidbody2D>().velocity = new Vector2(dir.x * force.x, dir.y * force.y);//需要与轨迹的力一致
        }
    }
}