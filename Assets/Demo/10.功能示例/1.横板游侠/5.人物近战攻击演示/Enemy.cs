using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.HB.Player.Attack
{
    /// <summary>
    /// ���˽ű�
    /// </summary>
    public class Enemy : MonoBehaviour
    {
        private int currentHealth = 5;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject psPrefab;

        private ParticleSystemController psController;

        void Start()
        {
            //��������ʱʹ�õ�����ϵͳ
            GameObject particleEffect = Instantiate(psPrefab, transform.position, Quaternion.identity);
            psController = particleEffect.GetComponent<ParticleSystemController>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// �յ�����
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
        /// ����Ч��
        /// </summary>
        private void HitEffect()
        {
            //�ı���ɫ
            StartCoroutine(ChangeColor());
        }

        /// <summary>
        /// �ı������ɫ
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
        /// ����
        /// </summary>
        private void Repel()
        {

        }

        /// <summary>
        /// ����
        /// </summary>
        private void Die()
        {
            Destroy(this.gameObject);
        }

        /// <summary>
        /// ����ʱ�ͷ�����
        /// </summary>
        protected void OnDestroy()
        {
            //��������
            psController.Play();
        }
    }
}