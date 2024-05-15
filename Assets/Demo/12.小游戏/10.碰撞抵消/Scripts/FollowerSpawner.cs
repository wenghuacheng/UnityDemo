using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.CollisionEliminate
{
    public abstract class FollowerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject followerPrefab;

        //����λ�ã�����ʱ���ȵ����������ɣ�
        private List<Vector3> spawnerPosition = new List<Vector3>() { Vector3.right, Vector3.up, Vector3.left, Vector3.back };
        private int spawnerPositionIndex = 0;

        /// <summary>
        /// ���ɸ�����
        /// </summary>
        public Follower GenerateFollower()
        {
            var position = spawnerPosition[spawnerPositionIndex++];
            spawnerPositionIndex = spawnerPositionIndex % spawnerPosition.Count;

            var obj = Instantiate(followerPrefab, this.transform.position + position, Quaternion.identity, this.transform);
            var follower = obj.GetComponent<Follower>();
            follower.SetTarget(this.transform);
            return follower;
        }
    }
}