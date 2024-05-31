using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ObjectMove
{
    /// <summary>
    /// 弹射子弹
    /// </summary>
    public class BulletEjection : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private LayerMask collisonLayerMask;

        private int maxEjectionCount = 5;//最大弹射次数
        private int maxPenetrationCount = 3;//最大穿透次数
        private int currentEjectionCount;
        private int currentPenetrationCount;

        private void Awake()
        {
            currentEjectionCount = maxEjectionCount;
            currentPenetrationCount = maxPenetrationCount;
        }

        private void Update()
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * 10);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var isCollison = (collision.gameObject.layer & (1 << collisonLayerMask)) > 0;
            var isEnemy = collision.gameObject.tag == "Enemy";
            //Debug.Log("isCollison：" + isCollison + "-isEnemy:" + isEnemy);
            if (isEnemy)
            {
                //穿透
                currentPenetrationCount--;
                if (currentPenetrationCount <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
            else if (isCollison)
            {
                ContactPoint2D[] contacts = new ContactPoint2D[1];
                collision.GetContacts(contacts);

                ContactPoint2D contact = contacts[0];
                var direction = Vector2.Reflect(this.transform.up, contact.normal);
                this.transform.up = direction;
                Debug.Log("弹射:" + direction);
            }
        }
    }
}
