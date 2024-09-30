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
        /// 数值变更
        /// </summary>
        private void Stats_OnStatsChanged()
        {
            UpdateStatsVisual();
        }

        /// <summary>
        /// 界面更新
        /// </summary>
        private void UpdateStatsVisual()
        {
            Mesh mesh = new Mesh();

            //雷达图点的最大长度
            int radarChartSize = 100;

            //顶点(包括5个数值点和一个原点)
            Vector3[] vertices = new Vector3[6];
            Vector2[] uv = new Vector2[6];
            //构建三角形(5个数值都是由一个三角形组成)
            int[] triangles = new int[3 * 5];

            //各个数值的位置索引
            int attackVertexIndex = 1;
            int defenceVertexIndex = 2;
            int speedVertexIndex = 3;
            int manaVertexIndex = 4;
            int healthVertexIndex = 5;

            //一个区域的旋转角度
            float angle = 360f / 5;

            /**
             * 构建每个数值位置对应界面所在的点
             * 说明：每个点基于比例计算出距离原点的值，再基于类型绕原点旋转对应角度
             * 例如：攻击值在原点正上方不需要旋转，防御值在原点右侧72度，需要向右旋转
             */
            Vector3 attackVector = Quaternion.Euler(0, 0, -angle * 0) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Attack);
            Vector3 defenceVector = Quaternion.Euler(0, 0, -angle * 1) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Defence);
            Vector3 speedVector = Quaternion.Euler(0, 0, -angle * 2) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Speed);
            Vector3 manaVector = Quaternion.Euler(0, 0, -angle * 3) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Mana);
            Vector3 healthVector = Quaternion.Euler(0, 0, -angle * 4) * Vector3.up * radarChartSize * stats.GetStateNormalized(Stats.StatType.Health);

            //设置顶点
            vertices[0] = Vector3.zero;
            vertices[attackVertexIndex] = attackVector;
            vertices[defenceVertexIndex] = defenceVector;
            vertices[speedVertexIndex] = speedVector;
            vertices[manaVertexIndex] = manaVector;
            vertices[healthVertexIndex] = healthVector;

            //三角形构建索引
            //攻击数值的三角形顶点索引
            triangles[0] = 0;
            triangles[1] = attackVertexIndex;
            triangles[2] = defenceVertexIndex;
            //防御数值的三角形顶点索引
            triangles[3] = 0;
            triangles[4] = defenceVertexIndex;
            triangles[5] = speedVertexIndex;
            //速度数值的三角形顶点索引
            triangles[6] = 0;
            triangles[7] = speedVertexIndex;
            triangles[8] = manaVertexIndex;
            //魔法数值的三角形顶点索引
            triangles[9] = 0;
            triangles[10] = manaVertexIndex;
            triangles[11] = healthVertexIndex;
            //生命数值的三角形顶点索引
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