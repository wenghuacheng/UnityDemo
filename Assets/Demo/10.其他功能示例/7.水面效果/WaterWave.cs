using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Demo.Other.Wave
{
    public class WaterWave : MonoBehaviour
    {
        [SerializeField] private float springStiffness = 0.1f;
        [SerializeField] private int wavesCount = 6;
        [SerializeField] private GameObject wavePointPrefab;
        [SerializeField] private Transform parent;


        private List<WaterSpring> springs = new();
        private SpriteShapeController spriteShapeController;

        private int corsnersCount = 2;//SpriteShape的顶点数量
        private float spread = 0.006f;//传播速度

        private void Awake()
        {
            spriteShapeController = GetComponent<SpriteShapeController>();

            SetWaves();
            CreateSpring(spriteShapeController.spline);
        }


        private void FixedUpdate()
        {
            foreach (WaterSpring spring in springs)
            {
                spring.WaveSpringDampeningUpdate(springStiffness, 0.03f);
                spring.WavePointUpdate();
            }
            UpdateSprings();
        }

        /// <summary>
        /// 创建海浪的控制点
        /// </summary>
        private void CreateSpring(Spline spline)
        {
            springs.Clear();

            float scale = 10f;

            for (int i = 0; i <= wavesCount + 1; i++)
            {
                int index = i + 1;
                var wavePoint = Instantiate(wavePointPrefab, parent, false);
                var pos = spline.GetPosition(index);
                wavePoint.transform.localPosition = new Vector3(pos.x * scale, pos.y, pos.z);
                WaterSpring waterSpring = wavePoint.GetComponent<WaterSpring>();
                waterSpring.Init(spriteShapeController, index);
                springs.Add(waterSpring);
            }
        }

        /// <summary>
        /// 更新传播速度
        /// 当其中一个点发生位移时，其左右的点也需要发生位移，且需要进行传播的递减
        /// </summary>
        private void UpdateSprings()
        {
            int count = springs.Count;
            float[] left_deltas = new float[count];
            float[] right_deltas = new float[count];

            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    left_deltas[i] = spread * (springs[i].height - springs[i - 1].height);
                    Splash(i - 1, left_deltas[i]);
                }

                if (i < count - 1)
                {
                    right_deltas[i] = spread * (springs[i].height - springs[i + 1].height);
                    Splash(i + 1, right_deltas[i]);
                }
            }
        }

        /// <summary>
        /// 刷新速度
        /// </summary>
        /// <param name="index"></param>
        /// <param name="speed"></param>
        private void Splash(int index, float speed)
        {
            if (index >= 0 && index < springs.Count)
            {
                springs[index].velocity += speed;
            }
        }

        /// <summary>
        /// 生成SpriteShape的控制点
        /// </summary>
        private void SetWaves()
        {
            Spline waterSpline = spriteShapeController.spline;
            int waterPointsCount = waterSpline.GetPointCount();

            #region 移除中间点（之前添加过的）
            for (int i = corsnersCount; i < waterPointsCount - corsnersCount; i++)
            {
                //没明白这边为什么这么写
                waterSpline.RemovePointAt(corsnersCount);
            }

            #endregion

            #region 添加点

            //获取左右边界的点
            Vector3 waterTopLeftCorner = waterSpline.GetPosition(1);
            Vector3 waterTopRightCorner = waterSpline.GetPosition(2);

            float waterWidth = waterTopRightCorner.x - waterTopLeftCorner.x;
            float spacingWare = waterWidth / (wavesCount + 1);//计算海浪间距

            for (int i = wavesCount; i > 0; i--)
            {
                int index = corsnersCount;

                //从右边往左插入可以省去计算索引值的麻烦
                var xPosition = waterTopLeftCorner.x + (spacingWare * i);
                var wavePoint = new Vector3(xPosition, waterTopLeftCorner.y, waterTopLeftCorner.z);
                waterSpline.InsertPointAt(index, wavePoint);
                waterSpline.SetHeight(index, 0.1f);
                waterSpline.SetCorner(index, false);

                //让两个控制点之间连接为弧形而不是直线，这样更有海浪的感觉
                waterSpline.SetTangentMode(index, ShapeTangentMode.Continuous);
            }

            #endregion
        }
    }
}