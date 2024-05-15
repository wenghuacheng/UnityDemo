using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.CollisionEliminate
{
    /// <summary>
    /// ��Ҷ���
    /// </summary>
    public class Player : FollowerSpawner
    {
        private List<Follower> Followers = new List<Follower>();
        private float _speed = 3;
        private bool isEnd = false;

        private void Start()
        {
            //Ĭ�����ɵĸ�����
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
        /// �ƶ�
        /// </summary>
        private void Movement()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            this.transform.Translate(new Vector2(x, y) * _speed * Time.deltaTime);
        }

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        public void CreateFollower()
        {
            var follower = GenerateFollower();
            follower.OnDestory += Follower_OnDestory;
            Followers.Add(follower);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="obj"></param>
        private void Follower_OnDestory(Follower follower)
        {
            follower.OnDestory -= Follower_OnDestory;
            Followers.Remove(follower);
            if (Followers.Count <= 0)
            {
                isEnd = true;
                Debug.Log("������");
            }
        }

    }
}