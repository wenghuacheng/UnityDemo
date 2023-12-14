using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.CollisionEliminate
{
    /// <summary>
    /// 玩家对象
    /// </summary>
    public class Player : FollowerSpawner
    {
        private List<Follower> Followers = new List<Follower>();
        private float _speed = 3;
        private bool isEnd = false;

        private void Start()
        {
            //默认生成的跟随者
            for (int i = 0; i < 5; i++)
            {
                CreateFollower();
            }
        }

        void Update()
        {
            if (!isEnd)
                Movement();
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            this.transform.Translate(new Vector2(x, y) * _speed * Time.deltaTime);
        }

        /// <summary>
        /// 初始化跟随者
        /// </summary>
        public void CreateFollower()
        {
            var follower = GenerateFollower();
            follower.OnDestory += Follower_OnDestory;
            Followers.Add(follower);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="obj"></param>
        private void Follower_OnDestory(Follower follower)
        {
            follower.OnDestory -= Follower_OnDestory;
            Followers.Remove(follower);
            if (Followers.Count <= 0)
            {
                isEnd = true;
                Debug.Log("结束了");
            }
        }

    }
}