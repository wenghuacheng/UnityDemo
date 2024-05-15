using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.CollisionEliminate
{
    /// <summary>
    /// ���˶��󣬰���һ�Ѹ�����
    /// </summary>
    public class Enemy : FollowerSpawner
    {
        [SerializeField] private int count = 20;

        void Start()
        {
            count = Random.Range(5, 15);
            for (int i = 0; i < count; i++)
            {
                GenerateFollower();
            }
        }

        void Update()
        {
            //var direction = (player.transform.position - this.transform.position).normalized;
            //this.transform.Translate(direction * Time.deltaTime);
        }
    }
}