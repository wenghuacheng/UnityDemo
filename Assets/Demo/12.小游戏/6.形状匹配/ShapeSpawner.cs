using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.MatchShapes
{
    public class ShapeSpawner : MonoBehaviour
    {
        private float time;
        [SerializeField] private float intervalTime = 1f;//形状生成间隔时间
        [SerializeField] private float shapeSpeed = 3;//形状移动速度
        [SerializeField] private GameObject shapePrefab;
        [SerializeField] private MatchShapeList shapeList;

        [SerializeField] private Transform startPositionTransform;//起始位置

        void Start()
        {

        }

        void Update()
        {
            GenerateShape();
        }

        /// <summary>
        /// 生成形状
        /// </summary>
        private void GenerateShape()
        {
            time -= Time.deltaTime;
            if (time > 0)
                return;

            //重置生成时间
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