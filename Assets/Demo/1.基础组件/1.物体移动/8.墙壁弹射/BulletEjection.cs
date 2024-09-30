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

        private void Update()
        {
            this.transform.Translate(Vector3.up * Time.deltaTime * 10);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var contact = collision.GetContact(0);
            var direction = Vector2.Reflect(this.transform.up, contact.normal).normalized;

            //防止垂直撞击墙面后原路返回，随机产生偏移量
            if (direction.x == 0)
                direction.x = Random.Range(0, 1) >= 0.5 ? Random.Range(-0.1f, -1) : Random.Range(0.1f, 1);
            else if (direction.y == 0)
                direction.y = Random.Range(0, 1) >= 0.5 ? Random.Range(-0.1f, -1) : Random.Range(0.1f, 1);

            direction = direction.normalized;
            this.transform.up = direction;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var isCollison = ((1 << collision.gameObject.layer) & collisonLayerMask) > 0;
            Debug.Log(isCollison);
        }
    }
}
