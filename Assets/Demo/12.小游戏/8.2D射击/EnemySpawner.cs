using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.Shoot2D
{
    /// <summary>
    /// ����������
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Block[] blocks;//�ϰ���
        [SerializeField] private GameObject outScreenSpawnerPosition;//��Ļ�����ɵ�

        private float spawnerTime = 0;
        private float spawnerIntervalTime =0.8f;

        private List<EnemyPath> paths = new List<EnemyPath>();//·��

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
        /// ���ɵ���
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
        /// ��ȡ���ɵ�λ��
        /// </summary>
        private void FillEnemyPathPositions()
        {
            //�ϰ������ɵ�
            foreach (var block in blocks)
            {
                paths.AddRange(block.pathList);
            }

            //��Ļ�����ɵ�
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
        /// �����ȡ����·��
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