using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class RadarChartUI : MonoBehaviour
    {
        private Stats stats;
        private CanvasRenderer canvasRenderer;

        [SerializeField] private Material material;
        [SerializeField] private Texture2D texture2D;

        private void Awake()
        {
            canvasRenderer = this.transform.Find("RadarMesh").GetComponent<CanvasRenderer>();
        }

        public void SetStats(Stats stats)
        {
            this.stats = stats;
            stats.OnStatsChanged += Stats_OnStatsChanged;
            UpdateStatsVisual();
        }

        /// <summary>
        /// ��ֵ���
        /// </summary>
        private void Stats_OnStatsChanged()
        {
            UpdateStatsVisual();
        }

        /// <summary>
        /// �������
        /// </summary>
        private void UpdateStatsVisual()
        {
            Mesh mesh = new Mesh();

            //�״�ͼ�����󳤶�
            int radarChartSize = 100;

            //����(����5����ֵ���һ��ԭ��)
            Vector3[] vertices = new Vector3[6];
            Vector2[] uv = new Vector2[6];
            //����������(5����ֵ������һ�����������)
            int[] triangles = new int[3 * 5];

            //������ֵ��λ������
            int attackVertexIndex = 1;
            int defenceVertexIndex = 2;
            int speedVertexIndex = 3;
            int manaVertexIndex = 4;
            int healthVertexIndex = 5;

            //һ���������ת�Ƕ�
            float angle = 360f / 5;

            /**
             * ����ÿ����ֵλ�ö�Ӧ�������ڵĵ�
             * ˵����ÿ������ڱ������������ԭ���ֵ���ٻ���������ԭ����ת��Ӧ�Ƕ�
             * ���磺����ֵ��ԭ�����Ϸ�����Ҫ��ת������ֵ��ԭ���Ҳ�72�ȣ���Ҫ������ת
             */
            Vector3 attackVector = Quaternion.Euler(0, 0, -angle * 0) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Attack);
            Vector3 defenceVector = Quaternion.Euler(0, 0, -angle * 1) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Defence);
            Vector3 speedVector = Quaternion.Euler(0, 0, -angle * 2) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Speed);
            Vector3 manaVector = Quaternion.Euler(0, 0, -angle * 3) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Mana);
            Vector3 healthVector = Quaternion.Euler(0, 0, -angle * 4) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Health);

            //���ö���
            vertices[0] = Vector3.zero;
            vertices[attackVertexIndex] = attackVector;
            vertices[defenceVertexIndex] = defenceVector;
            vertices[speedVertexIndex] = speedVector;
            vertices[manaVertexIndex] = manaVector;
            vertices[healthVertexIndex] = healthVector;

            //�����ι�������
            //������ֵ�������ζ�������
            triangles[0] = 0;
            triangles[1] = attackVertexIndex;
            triangles[2] = defenceVertexIndex;
            //������ֵ�������ζ�������
            triangles[3] = 0;
            triangles[4] = defenceVertexIndex;
            triangles[5] = speedVertexIndex;
            //�ٶ���ֵ�������ζ�������
            triangles[6] = 0;
            triangles[7] = speedVertexIndex;
            triangles[8] = manaVertexIndex;
            //ħ����ֵ�������ζ�������
            triangles[9] = 0;
            triangles[10] = manaVertexIndex;
            triangles[11] = healthVertexIndex;
            //������ֵ�������ζ�������
            triangles[12] = 0;
            triangles[13] = healthVertexIndex;
            triangles[14] = attackVertexIndex;

            //UV
            uv[0] = Vector2.zero;
            uv[attackVertexIndex] = Vector2.one;
            uv[defenceVertexIndex] = Vector2.one;
            uv[speedVertexIndex] = Vector2.one;
            uv[manaVertexIndex] = Vector2.one;
            uv[healthVertexIndex] = Vector2.one;

            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uv;

            canvasRenderer.SetMesh(mesh);
            canvasRenderer.SetMaterial(material, texture2D);
        }
    }
}