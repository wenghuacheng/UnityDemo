using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Attack
{
    /// <summary>
    /// 敌人脚本
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        private int currentHealth = 5;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject psPrefab;

        private ParticleSystemController psController;

        void Start()
        {
            //生成死亡时使用的粒子系统
            GameObject particleEffect = Instantiate(psPrefab, transform.position, Quaternion.identity);
            psController = particleEffect.GetComponent<ParticleSystemController>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// 收到攻击
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage)
        {
            HitEffect();

            currentHealth -= damage;
            if (currentHealth <= 0)
                Die();
        }

        /// <summary>
        /// 命中效果
        /// </summary>
        private void HitEffect()
        {
            //改变颜色
            StartCoroutine(ChangeColor());
        }

        /// <summary>
        /// 改变敌人颜色
        /// </summary>
        private IEnumerator ChangeColor()
        {
            var origionColor = spriteRenderer.color;
            for (int i = 0; i < 1; i++)
            {
                spriteRenderer.color = Color.red;
                yield return new WaitForSeconds(0.05f);
                spriteRenderer.color = origionColor;
            }
            yield return null;
        }

        /// <summary>
        /// 击退
        /// </summary>
        private void Repel()
        {

        }

        /// <summary>
        /// 死亡
        /// </summary>
        private void Die()
        {
            Destroy(this.gameObject);
        }

        /// <summary>
        /// 销毁时释放粒子
        /// </summary>
        protected void OnDestroy()
        {
            //播放粒子
            psController.Play();
        }
    }
}