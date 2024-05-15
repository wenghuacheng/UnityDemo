using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.BrickBreaker
{
    /// <summary>
    /// 小球
    /// </summary>
    public class Ball : MonoBehaviour
    {
        private float speed = 20;//小球移动速度

        //移动方向
        private Vector2 direction;

        void Start()
        {
            direction = new Vector2(0.5f, -0.5f);//初始方向
        }

        void Update()
        {
            Forward();
        }

        /// <summary>
        /// 前进
        /// </summary>
        private void Forward()
        {
            this.transform.Translate(direction * Time.deltaTime * speed);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            //接触点法线方向
            //特别注意，球体不能进行旋转，否则获取的接触面法线会有问题
            var normal = collision.contacts[0].normal;
            //计算反弹坐标
            var newDirection = Vector2.Reflect(direction, normal);
            direction = newDirection;
            Debug.Log("法线坐标：" + normal + "方向坐标:" + direction);
        }
    }
}