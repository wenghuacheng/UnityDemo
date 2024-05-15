using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public class EnemyAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private Enemy enemy;

        /// <summary>
        /// ¹¥»÷
        /// </summary>
        public void Attack()
        {
            enemy.Attack();
        }

    }
}
