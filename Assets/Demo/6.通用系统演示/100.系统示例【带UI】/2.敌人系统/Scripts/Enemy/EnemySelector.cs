using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 敌人被选中状态【UI控制】
    /// </summary>
    public class EnemySelector : MonoBehaviour
    {
        [SerializeField] private GameObject selectionObj;//选择的框
        private EnemyBrain enemyBrain;

        private void Awake()
        {
            enemyBrain = GetComponent<EnemyBrain>();
            selectionObj?.SetActive(false);

            EnemySelectorManager.OnEnemySelected += OnEnemySelectedHandler;
            EnemySelectorManager.OnEnemyUnSelected += OnEnemyUnSelectedHandler;
        }

        private void OnEnemySelectedHandler(EnemyBrain obj)
        {
            selectionObj.SetActive(enemyBrain == obj);
        }

        private void OnEnemyUnSelectedHandler()
        {
            selectionObj.SetActive(false);
        }
    }
}