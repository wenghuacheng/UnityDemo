using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace Demo.Meshes
{
    public class FieldRangerRender : MonoBehaviour
    {
        private MeshFilter meshFilter;
        private Camera _camera;

        //视角角度
        float fov = 90f;

        private void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            _camera = Camera.main;
        }

        void Update()
        {
            var dir = GetDirection();
            var angle = GetAngle(dir);

            angle += 90;

            DrawRange(angle - fov / 2);
        }


        private void DrawRange(float startAngle)
        {
            Mesh mesh = new Mesh();
            meshFilter.mesh = mesh;

            var originPosition = Vector3.zero;

            //扇区数量
            int areaCount = 60;
            //每个扇区的角度
            float angleIncrease = fov / areaCount;
            //射线长度
            float distance = 2f;

            //顶点数组=扇区数量+原点+1
            Vector3[] vertices = new Vector3[areaCount + 1 + 1];
            //三角形顶点索引数组
            int[] triangles = new int[areaCount * 3];
            //uv数组
            Vector2[] uv = new Vector2[vertices.Count()];

            #region 设置顶点
            vertices[0] = originPosition;//第一个点为起始点【原点】
            for (int i = 1; i < vertices.Length; i++)
            {
                //注意需要是负角度才是顺时针，否则是逆时针
                //而渲染mesh时需要为顺时针，否则不会渲染
                var angle = startAngle + angleIncrease * (i - 1) * -1;

                //获取旋转后的点坐标
                var target = GetRotationPosition(angle);
                var raycastHit = Physics2D.Raycast(originPosition, target, distance);

                if (raycastHit.collider == null)
                {
                    //没有检测到物体
                    vertices[i] = originPosition + target * distance;
                }
                else
                {
                    //检测到物体使用碰撞点
                    vertices[i] = raycastHit.point;
                }


            }
            #endregion

            #region 设置三角形
            //顶点索引
            int verticesIndex = 1;
            for (int i = 0; i < triangles.Length; i += 3)
            {
                triangles[i] = 0;
                triangles[i + 1] = verticesIndex;
                triangles[i + 2] = verticesIndex + 1;
                verticesIndex++;
            }
            #endregion

            #region 测试
            //for (int i = 0; i < vertices.Length; i++)
            //{
            //    Debug.Log("顶点:" + vertices[i]);
            //}

            //for (int i = 0; i < triangles.Length; i++)
            //{
            //    Debug.Log("三角形索引:" + triangles[i]);
            //}
            #endregion

            //设置mesh值
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
        }

        /// <summary>
        /// 获取旋转后的角度坐标
        /// </summary>
        /// <param name="angle"></param>
        private Vector3 GetRotationPosition(float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
        }

        /// <summary>
        /// 获取当前向量与零点的角度
        /// </summary>
        /// <returns></returns>
        private float GetAngle(Vector3 dir)
        {
            // -180° 到 180°
            var rad = Mathf.Atan2(dir.y, dir.x);
            return rad * Mathf.Rad2Deg;
        }

        /// <summary>
        /// 获取鼠标方向
        /// </summary>
        /// <returns></returns>
        public Vector3 GetDirection()
        {
            var m = _camera.ScreenToWorldPoint(Input.mousePosition);
            //这里Vector3.zero为人物或物体起始点坐标
            return m - Vector3.zero;
        }

    }
}