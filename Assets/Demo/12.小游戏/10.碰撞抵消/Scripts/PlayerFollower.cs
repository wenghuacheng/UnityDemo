using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.CollisionEliminate
{
    /// <summary>
    /// ��Ҹ�����
    /// ���Լ�����֤һ����������һ��
    /// </summary>
    public class PlayerFollower : Follower
    {
        private bool isDestoryed = false;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isDestoryed) return;

            var follower = collision.collider.GetComponent<Follower>();
            if (follower != null && follower._target != _target)
            {
                isDestoryed = true;
                RaiseFollowerDestory();
                Destroy(this.gameObject);
                Destroy(follower.gameObject);
            }
        }
    }
}