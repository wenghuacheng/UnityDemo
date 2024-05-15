using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ѡ�����
    /// </summary>
    public class EnemySelectorManager : MonoBehaviour
    {
        [SerializeField] private LayerMask enemyMask;
        private Camera mainCamera;
        private EnemyBrain curEnemy;//��ǰѡ�еĵ���

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
        /// ѡ�е���
        /// </summary>
        private void SelectedEnemy()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //��������ѡ�����
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