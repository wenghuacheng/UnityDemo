using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.Shoot2D
{
    /// <summary>
    /// 敌人生成器
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Block[] blocks;//障碍物
        [SerializeField] private GameObject outScreenSpawnerPosition;//屏幕外生成点

        private float spawnerTime = 0;
        private float spawnerIntervalTime =0.8f;

        private List<EnemyPath> paths = new List<EnemyPath>();//路径

        void Start()
        {
            FillEnemyPathPositions();
            GenerateEnemy();
        }

        void Update()
        {
            spawnerTime -= Time.deltaTime;
            if (spawnerTime <= 0)
            {
                spawnerTime = spawnerIntervalTime;
                GenerateEnemy();
            }
        }

        /// <summary>
        /// 生成敌人
        /// </summary>
        private void GenerateEnemy()
        {
            var enemyPath = RandomAvailableEnemyPath();
            if (enemyPath == null) return;

            var enemyObj = Instantiate(enemyPrefab, enemyPath.GetStart(), Quaternion.identity, this.transform);
            var enemy = enemyObj.GetComponent<Enemy>();
            enemy.Initialized(enemyPath);
        }

        /// <summary>
        /// 获取生成点位置
        /// </summary>
        private void FillEnemyPathPositions()
        {
            //障碍物生成点
            foreach (var block in blocks)
            {
                paths.AddRange(block.pathList);
            }

            //屏幕外生成点
            for (int i = 0; i < outScreenSpawnerPosition.transform.childCount; i++)
            {
                EnemyPath path = new EnemyPath();
                path.needRotation = false;
                var child = outScreenSpawnerPosition.transform.GetChild(i);
                for (int j = 0; j < child.childCount; j++)
                {
                    var p = child.GetChild(j);
                    path.Positions.Add(p.transform.position);
                }
                paths.Add(path);
            }
        }

        /// <summary>
        /// 随机获取可用路径
        /// </summary>
        private EnemyPath RandomAvailableEnemyPath()
        {
            var availablePaths = paths.Where(p => !p.isUsed).ToList();
            if (paths.Count <= 0) return null;
            var index = Random.Range(0, availablePaths.Count());
            if (index >= availablePaths.Count) return null;

            return availablePaths[index];
        }
    }
}