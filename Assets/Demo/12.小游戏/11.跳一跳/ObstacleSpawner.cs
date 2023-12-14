using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.JumpAJump
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject obstaclePrefab;
        [SerializeField] private Transform startPosition;

        void Start()
        {
            StartCoroutine(Generate());
        }

        /// <summary>
        /// 生成障碍物
        /// </summary>
        /// <returns></returns>
        private IEnumerator Generate()
        {
            while (true)
            {
                var time = Random.Range(0.5f, 2f);
                yield return new WaitForSeconds(time);

                //生成障碍物
                Instantiate(obstaclePrefab, startPosition.position, Quaternion.identity, this.transform);
            }
        }
    }
}