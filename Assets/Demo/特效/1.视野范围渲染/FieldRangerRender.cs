using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using static UnityEngine.UI.Image;

public class FieldRangerRender : MonoBehaviour
{
    private MeshFilter meshFilter;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    void Start()
    {
        //DrawTriangle();
        DrawRange();
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void DrawRange()
    {
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        //视角
        float fov = 90f;
        //射线数量【至少需要2条射线形成一个三角形】
        int rayCount = 2;

        float angle = 0;
        float intervalAngle = fov / (rayCount - 1);//每个扇区的角度
        float viewDistance = 50f;

        //顶点数组=扇区数量+原点+1
        Vector3[] vertices = new Vector3[rayCount + 1];
        //三角形顶点索引数组
        int[] triangles = new int[(rayCount - 1) * 3];
        //uv数组
        Vector2[] uv = new Vector2[vertices.Count()];

        #region 顶点
        //从第二个位置开始，因为第一个坐标为(0,0)点
        angle += intervalAngle;
        for (int i = 1; i <= rayCount; i++)
        {
            vertices[i] = new Vector3(1f * i, 1f * i, 0);
        }
        #endregion

        #region 三角形
        int verticesIndex = 1;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            triangles[i] = 0;
            triangles[i + 1] = verticesIndex;
            triangles[i + 2] = verticesIndex + 1;
            verticesIndex++;
        }

        #endregion
        Debug.Log(vertices.Length);
        for (int i = 0; i < vertices.Count(); i++)
        {
            Debug.Log("V" + vertices[i]);
        }


        Debug.Log(triangles.Length);
        for (int i = 0; i < triangles.Length; i++)
        {
            Debug.Log("T" + triangles[i]);
        }

        //设置mesh值
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;



    }

    /// <summary>
    /// 最简单的mesh
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
        vertices[0] = Vector3.zero;
        vertices[1] = new Vector3(1, 0);
        vertices[2] = new Vector3(0, 1);
        #endregion

        #region 设置三角形索引
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        #endregion

        //设置mesh值
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;



    }



    private void OnDrawGizmos()
    {
        ////区域数量
        //int areaCount = 10;
        ////视角范围
        //float angle = 90f;
        ////增加角度
        //float angleIncrease = 90 / areaCount;

        //float currentAngle = 0;
        //for (int i = 0; i < areaCount; i++)
        //{
        //    Vector3 origin = new Vector2(0, 0);
        //    Vector3 point = new Vector2(1, 0);

        //    var rotation = Quaternion.Euler(0, 0, currentAngle);
        //    var rotatedPoint = rotation * (point - origin) + origin;

        //    Gizmos.DrawLine(Vector3.zero, rotatedPoint);

        //    currentAngle += angleIncrease;
        //}

    }
}
