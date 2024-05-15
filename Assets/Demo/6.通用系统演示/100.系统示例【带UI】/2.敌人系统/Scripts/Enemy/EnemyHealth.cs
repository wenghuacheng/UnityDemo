using Demo.Common.PlayerSysWithUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ��������
    /// </summary>
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [Header("Config")]
        [SerializeField] private float health;

        private Animator animator;
        private EnemyBrain enemyBrain;
        private EnemySelector enemySelector;

        public float CurrentHealth { get; set; }

        private void Awake()
        {
            CurrentHealth = health;
            animator = GetComponent<Animator>();
            enemyBrain = GetComponent<EnemyBrain>();
            enemySelector = GetComponent<EnemySelector>();
        }

        public void TakeDamage(float amount)
        {
            CurrentHealth -= amount;
            if (CurrentHealth < 0)
            {
                animator.SetTrigger("Death");
                enemyBrain.enabled = false;
                enemySelector.OnEnemyUnSelectedHandler();//��Ҫѡ��״̬
            }
            else
            {
                DamageManager.Instance.ShowDamageText(amount, this.transform);
            }
        }
    }
}