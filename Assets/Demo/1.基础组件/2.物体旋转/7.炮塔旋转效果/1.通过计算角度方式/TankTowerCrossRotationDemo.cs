using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// 基于点乘计算的炮塔旋转效果
    /// </summary>
    public class TankTowerCrossRotationDemo : MonoBehaviour
    {
        [SerializeField] private Transform tower;

        private Camera _camera;

        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            //获取鼠标的所在的世界坐标点
            var position = Input.mousePosition;
            var worldPosition = _camera.ScreenToWorldPoint(position);

            //获取旋转向量
            var rotateVector = GetRotateVector(tower, worldPosition);
            if (rotateVector == Vector3.zero) return;

            //基于旋转向量进行旋转
            tower.Rotate(rotateVector * 50 * Time.deltaTime);
        }


        /// <summary>
        /// 获取旋转的向量
        /// </summary>
        /// <param name="self"></param>
        /// <param name="targetPostion"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static Vector3 GetRotateVector(Transform self, Vector3 targetPostion)
        {
            // 计算物体指向鼠标位置的向量
            // 目标位置-当前位置才是移动向量
            var direction = (targetPostion - self.position).normalized;


            //计算炮管与目标位置的夹角
            //计算出的角度是正负【即没有方向】
            var angle = Vector2.Angle(direction, self.up);

            if (Mathf.Approximately(angle, 0)) return Vector3.zero;

            //unity中逆时针角度是正方向
            var rotateVector = new Vector3(0, 0, 1);

            //通过叉乘计算出旋转方向
            var from = self.up.normalized;
            var to = direction.normalized;
            var cross = Vector3.Cross(from, to);

            //注意：2d和3d判断条件不一样
            //此外注意相机类型正交
            if (cross.z < 0)
            {
                rotateVector = rotateVector * -1;
            }

            return rotateVector;
        }
    }
}