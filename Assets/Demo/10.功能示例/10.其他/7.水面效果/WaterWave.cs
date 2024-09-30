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

        private int corsnersCount = 2;//SpriteShape�Ķ�������
        private float spread = 0.006f;//�����ٶ�

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
        /// �������˵Ŀ��Ƶ�
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
        /// ���´����ٶ�
        /// ������һ���㷢��λ��ʱ�������ҵĵ�Ҳ��Ҫ����λ�ƣ�����Ҫ���д����ĵݼ�
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
        /// ˢ���ٶ�
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
        /// ����SpriteShape�Ŀ��Ƶ�
        /// </summary>
        private void SetWaves()
        {
            Spline waterSpline = spriteShapeController.spline;
            int waterPointsCount = waterSpline.GetPointCount();

            #region �Ƴ��м�㣨֮ǰ��ӹ��ģ�
            for (int i = corsnersCount; i < waterPointsCount - corsnersCount; i++)
            {
                //û�������Ϊʲô��ôд
                waterSpline.RemovePointAt(corsnersCount);
            }

            #endregion

            #region ��ӵ�

            //��ȡ���ұ߽�ĵ�
            Vector3 waterTopLeftCorner = waterSpline.GetPosition(1);
            Vector3 waterTopRightCorner = waterSpline.GetPosition(2);

            float waterWidth = waterTopRightCorner.x - waterTopLeftCorner.x;
            float spacingWare = waterWidth / (wavesCount + 1);//���㺣�˼��

            for (int i = wavesCount; i > 0; i--)
            {
                int index = corsnersCount;

                //���ұ�����������ʡȥ��������ֵ���鷳
                var xPosition = waterTopLeftCorner.x + (spacingWare * i);
                var wavePoint = new Vector3(xPosition, waterTopLeftCorner.y, waterTopLeftCorner.z);
                waterSpline.InsertPointAt(index, wavePoint);
                waterSpline.SetHeight(index, 0.1f);
                waterSpline.SetCorner(index, false);

                //���������Ƶ�֮������Ϊ���ζ�����ֱ�ߣ��������к��˵ĸо�
                waterSpline.SetTangentMode(index, ShapeTangentMode.Continuous);
            }

            #endregion
        }
    }
}