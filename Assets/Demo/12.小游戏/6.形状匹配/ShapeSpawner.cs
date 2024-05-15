using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    public class ShapeSpawner : MonoBehaviour
    {
        private float time;
        [SerializeField] private float intervalTime = 1f;//��״���ɼ��ʱ��
        [SerializeField] private float shapeSpeed = 3;//��״�ƶ��ٶ�
        [SerializeField] private GameObject shapePrefab;
        [SerializeField] private MatchShapeList shapeList;

        [SerializeField] private Transform startPositionTransform;//��ʼλ��

        void Start()
        {

        }

        void Update()
        {
            GenerateShape();
        }

        /// <summary>
        /// ������״
        /// </summary>
        private void GenerateShape()
        {
            time -= Time.deltaTime;
            if (time > 0)
                return;

            //��������ʱ��
            time = intervalTime;

            int index = Random.Range(0, shapeList.list.Count());
            var matchShape = shapeList.list[index];

            var obj = Instantiate(shapePrefab, startPositionTransform.position, Quaternion.identity, this.transform);
            var shape = obj.GetComponent<Shape>();
            if (shape != null)
                shape.Initialize(matchShape, shapeSpeed);
        }
    }
}