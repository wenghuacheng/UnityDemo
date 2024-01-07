using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Games.DefendWall
{
    public class AIPlayer : MonoBehaviour
    {
        [SerializeField] private Transform firePosition;//发射位置
        [SerializeField] private AIBullet bulletPrefab;//弹药预制体

        [SerializeField] private LayerMask whatIsEnemy;

        //弹药发射
        private float maxFireTime = 1f;
        private float fireTime = 0;

        //当前敌人
        private Enemy curEnemy;

        void Update()
        {
            Fire();
        }

        #region 发射弹药
        private void Fire()
        {
            fireTime -= Time.deltaTime;
            if (fireTime >= 0) return;

            var enemy = FindEnemy();
            if (enemy == null) return;

            fireTime = maxFireTime;
            var bullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity, null);
            bullet.SetEnemy(enemy);
        }

        #endregion

        /// <summary>
        /// 查找敌人
        /// </summary>
        private Enemy FindEnemy()
        {
            if (curEnemy != null && !curEnemy.gameObject.IsDestroyed())
                return curEnemy;

            var distance = 30;
            var collider = Physics2D.OverlapCircle(this.transform.position, distance, whatIsEnemy);
            if (collider == null) return null;
            return collider.GetComponent<Enemy>();
        }
    }
}