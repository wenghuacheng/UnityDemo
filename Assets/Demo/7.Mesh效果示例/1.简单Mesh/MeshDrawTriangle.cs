using UnityEngine;

namespace Demo.Meshes
{
    public class MeshDrawTriangle: MonoBehaviour
    {
        private MeshFilter meshFilter;

        private void Start()
        {
            meshFilter = GetComponent<MeshFilter>();
            //画三角形
            DrawTriangle();
        }

        /// <summary>
        /// 画三角
        /// </summary>
        private void DrawTriangle()
        {
            Mesh mesh = new Mesh();
            meshFilter.mesh = mesh;

            //顶点数组
            Vector3[] vertices = new Vector3[3];
            //三角形顶点索引数组
            int[] triangles = new int[3];
            //uv数组
            Vector2[] uv = new Vector2[3];

            #region 设置顶点
            //顺时针绘制时才是正面，逆时针认为是背面不渲染
            vertices[0] = Vector3.zero;
            vertices[1] = new Vector3(0, 1);
            vertices[2] = new Vector3(1, 0);
            #endregion

            #region 设置三角形索引
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            #endregion

            //设置mesh值
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;        }

    }
}
