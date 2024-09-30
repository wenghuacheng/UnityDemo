using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Meshes
{
    public class MeshDrawRect : MonoBehaviour   
    {
        private MeshFilter meshFilter;

        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            //画三角形
            DrawRect();
        }

        /// <summary>
        /// 画正方形
        /// </summary>
        private void DrawRect()
        {
            Mesh mesh = new Mesh();
            meshFilter.mesh = mesh;

            //顶点数组
            Vector3[] vertices = new Vector3[4];
            //三角形顶点索引数组
            int[] triangles = new int[6];
            //uv数组
            Vector2[] uv = new Vector2[3];

            #region 设置顶点
            //顺时针绘制时才是正面，逆时针认为是背面不渲染
            vertices[0] = new Vector3(0, 0);
            vertices[1] = new Vector3(0, 1);
            vertices[2] = new Vector3(1, 0);
            vertices[3] = new Vector3(1, 1);
            #endregion

            #region 设置三角形索引
            //第一个三角形
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;

            //第二个三角形
            triangles[3] = 1;
            triangles[4] = 3;
            triangles[5] = 2;
            #endregion

            //设置mesh值
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
        }
    }
}
