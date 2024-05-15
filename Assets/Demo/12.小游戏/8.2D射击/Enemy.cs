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
        private bool isBackToBlock;//是否返回障碍物

        private float speed = 3f;//移动速度
        private float time = 0;
        private float waitTime = 3f;//等待时间

        public int maxHealth = 3;//最大生命值
        public int health = 3;//生命值

        [SerializeField] private Image percentImage;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="path"></param>
        public void Initialized(EnemyPath path)
        {
            path.isUsed = true;
            this.enemyPath = path;
            direction = path.GetEnd() - path.GetStart();

            if (path.needRotation)
                this.transform.up = direction;//确定移动方向

            //Debug.Log($"{ path.GetStart()}-{ path.GetEnd()}");
        }

        void Update()
        {
            Movement();
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Movement()
        {
            if (!isBackToBlock)
            {
                //从障碍物出来

                //PS:如果有距离判断就不要用Translate，因为可能导致移动超过目标位置而无限移动
                if (Vector2.Distance(this.transform.position, enemyPath.GetEnd()) <= 0.3f)
                {
                    //到达位置
                    time = waitTime;
                    isBackToBlock = true;
                }
                else
                {
                    //移动位置
                    this.transform.position = Vector2.MoveTowards(this.transform.position, enemyPath.GetEnd(), speed * Time.deltaTime);
                }
            }
            else
            {
                //从障碍物回来

                if (time > 0)
                {
                    time -= Time.deltaTime;
                }
                else
                {
                    //时间到了，回撤
                    this.transform.position = Vector2.MoveTowards(this.transform.position, enemyPath.GetStart(), speed * Time.deltaTime);

                    if (Vector2.Distance(this.transform.position, enemyPath.GetStart()) <= 0.3f)
                    {
                        //退回原始位置，销毁
                        Destroy(this.gameObject);
                    }
                }
            }
        }

        #region 生命与伤害
        public int TakeDamage(int damage = 1)
        {
            health -= damage;
            //刷新血条
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