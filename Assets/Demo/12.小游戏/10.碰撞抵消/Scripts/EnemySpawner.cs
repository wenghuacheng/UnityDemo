using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.CollisionEliminate
{
    /// <summary>
    /// Éú³ÉµÐÈË
    /// </summary>
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform[] positions;
        [SerializeField] private GameObject enemyPrefab;

        void Start()
        {
            for (int i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                Instantiate(enemyPrefab, position.position, Quaternion.identity, this.transform);
            }
        }
    }
}