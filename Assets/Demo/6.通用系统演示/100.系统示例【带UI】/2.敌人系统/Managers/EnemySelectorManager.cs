using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 选择敌人
    /// </summary>
    public class EnemySelectorManager : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyMask;
        private Camera mainCamera;
        private EnemyBrain curEnemy;//当前选中的敌人

        public static event Action<EnemyBrain> OnEnemySelected;
        public static event Action OnEnemyUnSelected;

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            SelectedEnemy();
        }

        /// <summary>
        /// 选中敌人
        /// </summary>
        private void SelectedEnemy()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //基于射线选择敌人
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                var collider = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, enemyMask);
                if (collider.collider != null)
                {
                    curEnemy = collider.collider.GetComponent<EnemyBrain>();
                    OnEnemySelected?.Invoke(curEnemy);
                }
                else
                {
                    curEnemy = null;
                    OnEnemyUnSelected?.Invoke();
                }
            }
        }
    }
}