using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Basic.LineDemo
{
    public class LineDrawCircle : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;
        [SerializeField] private SpriteRenderer sprite;

        private int positionCount = 360;
        private float radius = 3;

        void Start()
        {
            line.useWorldSpace = false;
            line.positionCount = positionCount;
            line.loop = true;

            sprite.transform.localScale = new Vector3(radius * 2, radius * 2);

            DrawCircle();
        }

        private void Update()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            this.transform.Translate(new Vector3(x, y) * Time.deltaTime);
        }

        private void DrawCircle()
        {
            Vector3[] points = new Vector3[positionCount];

            for (int i = 0; i < line.positionCount; i++)
            {
                var pos = GetRotationPosition(i, this.transform.position, radius);
                points[i] = pos;
            }
            line.SetPositions(points);
        }

        public Vector2 GetRotationPosition(float angle, Vector3 originPos, float distance)
        {
            //���Ƕ�ת��Ϊ����
            var radian = angle * Mathf.Deg2Rad;
            //ͨ�����Ǻ�������
            //Yֵ���Ա߼�sin��Xֵ���ٱ߼�cos
            var direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));

            return originPos + direction * distance;
        }

    }
}