using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ���˱�ѡ��״̬��UI���ơ�
    /// </summary>
    public class EnemySelector : MonoBehaviour
    {
        [SerializeField] private GameObject selectionObj;//ѡ��Ŀ�
        private EnemyBrain enemyBrain;

        private void Awake()
        {
            enemyBrain = GetComponent<EnemyBrain>();
            selectionObj?.SetActive(false);

            EnemySelectorManager.OnEnemySelected += OnEnemySelectedHandler;
            EnemySelectorManager.OnEnemyUnSelected += OnEnemyUnSelectedHandler;
        }

        public void OnEnemySelectedHandler(EnemyBrain obj)
        {
            selectionObj.SetActive(enemyBrain == obj);
        }

        public void OnEnemyUnSelectedHandler()
        {
            selectionObj.SetActive(false);
        }
    }
}