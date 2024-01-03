using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.DefendWall
{
    /// <summary>
    /// 敌人生成器
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private Transform spawnerPos;

        private float maxSpawnerTime = 0.6f;
        private float spawnerTime = 0f;

        //屏幕区域相关坐标
        private Camera mainCamera;
        private Vector2 leftTopPos;
        private Vector2 rightTopPos;
        private float minX = 0;
        private float maxX = 0;

        #region 初始化
        void Start()
        {
            mainCamera = Camera.main;
            InitilizeMovementArea();
        }

        /// <summary>
        /// 初始化玩家移动区域[左右]
        /// </summary>
        private void InitilizeMovementArea()
        {
            var renderer = enemyPrefab.GetComponent<SpriteRenderer>();
            var width = renderer.bounds.size.x;

            leftTopPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 1));
            rightTopPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 1));

            minX = leftTopPos.x + width / 2;
            maxX = rightTopPos.x - width / 2;
        }

        #endregion

        void Update()
        {
            spawnerTime -= Time.deltaTime;
            if (spawnerTime <= 0)
            {
                spawnerTime = maxSpawnerTime;
                GenerateEnemy();
            }
        }

        /// <summary>
        /// 生成敌人
        /// </summary>
        private void GenerateEnemy()
        {
            var x = Random.Range(minX, maxX);
            var pos = new Vector2(x, spawnerPos.position.y);
            Instantiate(enemyPrefab, pos, Quaternion.identity, null);
        }
    }
}