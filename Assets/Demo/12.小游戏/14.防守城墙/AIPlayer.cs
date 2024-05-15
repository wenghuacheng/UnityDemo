using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Games.DefendWall
{
    public class AIPlayer : MonoBehaviour
    {
        [SerializeField] private Transform firePosition;//����λ��
        [SerializeField] private AIBullet bulletPrefab;//��ҩԤ����

        [SerializeField] private LayerMask whatIsEnemy;

        //��ҩ����
        private float maxFireTime = 1f;
        private float fireTime = 0;

        //��ǰ����
        private Enemy curEnemy;

        void Update()
        {
            Fire();
        }

        #region ���䵯ҩ
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
        /// ���ҵ���
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