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

        //�ӽǽǶ�
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

            //��������
            int areaCount = 60;
            //ÿ�������ĽǶ�
            float angleIncrease = fov / areaCount;
            //���߳���
            float distance = 2f;

            //��������=��������+ԭ��+1
            Vector3[] vertices = new Vector3[areaCount + 1 + 1];
            //�����ζ�����������
            int[] triangles = new int[areaCount * 3];
            //uv����
            Vector2[] uv = new Vector2[vertices.Count()];

            #region ���ö���
            vertices[0] = originPosition;//��һ����Ϊ��ʼ�㡾ԭ�㡿
            for (int i = 1; i < vertices.Length; i++)
            {
                //ע����Ҫ�Ǹ��ǶȲ���˳ʱ�룬��������ʱ��
                //����Ⱦmeshʱ��ҪΪ˳ʱ�룬���򲻻���Ⱦ
                var angle = startAngle + angleIncrease * (i - 1) * -1;

                //��ȡ��ת��ĵ�����
                var target = GetRotationPosition(angle);
                var raycastHit = Physics2D.Raycast(originPosition, target, distance);

                if (raycastHit.collider == null)
                {
                    //û�м�⵽����
                    vertices[i] = originPosition + target * distance;
                }
                else
                {
                    //��⵽����ʹ����ײ��
                    vertices[i] = raycastHit.point;
                }


            }
            #endregion

            #region ����������
            //��������
            int verticesIndex = 1;
            for (int i = 0; i < triangles.Length; i += 3)
            {
                triangles[i] = 0;
                triangles[i + 1] = verticesIndex;
                triangles[i + 2] = verticesIndex + 1;
                verticesIndex++;
            }
            #endregion

            #region ����
            //for (int i = 0; i < vertices.Length; i++)
            //{
            //    Debug.Log("����:" + vertices[i]);
            //}

            //for (int i = 0; i < triangles.Length; i++)
            //{
            //    Debug.Log("����������:" + triangles[i]);
            //}
            #endregion

            //����meshֵ
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;
        }

        /// <summary>
        /// ��ȡ��ת��ĽǶ�����
        /// </summary>
        /// <param name="angle"></param>
        private Vector3 GetRotationPosition(float angle)
        {
            var rad = angle * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad));
        }

        /// <summary>
        /// ��ȡ��ǰ���������ĽǶ�
        /// </summary>
        /// <returns></returns>
        private float GetAngle(Vector3 dir)
        {
            // -180�� �� 180��
            var rad = Mathf.Atan2(dir.y, dir.x);
            return rad * Mathf.Rad2Deg;
        }

        /// <summary>
        /// ��ȡ��귽��
        /// </summary>
        /// <returns></returns>
        public Vector3 GetDirection()
        {
            var m = _camera.ScreenToWorldPoint(Input.mousePosition);
            //����Vector3.zeroΪ�����������ʼ������
            return m - Vector3.zero;
        }

    }
}