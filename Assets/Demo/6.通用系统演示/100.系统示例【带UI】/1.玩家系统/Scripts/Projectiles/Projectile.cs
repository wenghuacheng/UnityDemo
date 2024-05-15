using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// Ͷ���ﵯҩ
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private float speed;

        public Vector3 Direction { get; set; }
        public float Damage { get; set; }
        public GameObject Owner { get; set; }

        void Update()
        {
            this.transform.Translate(Direction * speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Owner == collision.gameObject) return;//���Լ������

            ///��ײ����˺�
            var damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(Damage);
                Destroy(this.gameObject);
            }
        }
    }
}