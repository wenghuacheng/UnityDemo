using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Games.Shoot2D
{
    public class Enemy : MonoBehaviour
    {
        private EnemyPath enemyPath;

        private Vector2 direction;
        private bool isBackToBlock;//�Ƿ񷵻��ϰ���

        private float speed = 3f;//�ƶ��ٶ�
        private float time = 0;
        private float waitTime = 3f;//�ȴ�ʱ��

        public int maxHealth = 3;//�������ֵ
        public int health = 3;//����ֵ

        [SerializeField] private Image percentImage;

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="path"></param>
        public void Initialized(EnemyPath path)
        {
            path.isUsed = true;
            this.enemyPath = path;
            direction = path.GetEnd() - path.GetStart();

            if (path.needRotation)
                this.transform.up = direction;//ȷ���ƶ�����

            //Debug.Log($"{ path.GetStart()}-{ path.GetEnd()}");
        }

        void Update()
        {
            Movement();
        }

        /// <summary>
        /// �ƶ�
        /// </summary>
        private void Movement()
        {
            if (!isBackToBlock)
            {
                //���ϰ������

                //PS:����о����жϾͲ�Ҫ��Translate����Ϊ���ܵ����ƶ�����Ŀ��λ�ö������ƶ�
                if (Vector2.Distance(this.transform.position, enemyPath.GetEnd()) <= 0.3f)
                {
                    //����λ��
                    time = waitTime;
                    isBackToBlock = true;
                }
                else
                {
                    //�ƶ�λ��
                    this.transform.position = Vector2.MoveTowards(this.transform.position, enemyPath.GetEnd(), speed * Time.deltaTime);
                }
            }
            else
            {
                //���ϰ������

                if (time > 0)
                {
                    time -= Time.deltaTime;
                }
                else
                {
                    //ʱ�䵽�ˣ��س�
                    this.transform.position = Vector2.MoveTowards(this.transform.position, enemyPath.GetStart(), speed * Time.deltaTime);

                    if (Vector2.Distance(this.transform.position, enemyPath.GetStart()) <= 0.3f)
                    {
                        //�˻�ԭʼλ�ã�����
                        Destroy(this.gameObject);
                    }
                }
            }
        }

        #region �������˺�
        public int TakeDamage(int damage = 1)
        {
            health -= damage;
            //ˢ��Ѫ��
            percentImage.fillAmount = health / (float)maxHealth;
            if (health <= 0)
                Destroy(this.gameObject);
            return health;
        }

        #endregion

        private void OnDestroy()
        {
            this.enemyPath.isUsed = false;
        }
    }
}