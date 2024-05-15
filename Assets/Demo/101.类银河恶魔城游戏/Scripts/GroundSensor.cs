using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.MetroidvaniaGame
{
    /// <summary>
    /// 地形判定器
    /// 注意：GroundSensor对象的layer，并且设定为与Player不碰撞
    /// </summary>
    public class GroundSensor : MonoBehaviour
    {
        //layer中使用的图层，这里使用的是Ground=6，Platform=9，分别代表地面和平台
        public const int SolidLayerValue = 6;
        public const int PlatformLayerValue = 9;

        //碰撞类型  0：Capsule  1：box  2：circle
        [SerializeField] private int collisionType;

        private Vector2 colliderSize_Origin;//原始碰撞范围
        private CapsuleCollider2D capsule;//碰撞类型=0时使用的碰撞体
        private BoxCollider2D box;//碰撞类型=1时使用的碰撞体
        private CircleCollider2D circle;//碰撞类型=2时使用的碰撞体

        private Rigidbody2D body;

        [SerializeField] private bool onGround;//是否在地板上
        [SerializeField] private bool onOneWay;//是否在平台上

        private LayerMask solidMask = 1 << SolidLayerValue;//地板图层
        private LayerMask oneWayMask = 1 << PlatformLayerValue;//平台图层(跳台)
        private LayerMask layerMask = 1 << SolidLayerValue | 1 << PlatformLayerValue;//地板图层&平台图层

        #region 初始化
        private void Awake()
        {
            body = this.transform.parent.GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            switch (collisionType)
            {
                case 0:
                    capsule = this.GetComponent<CapsuleCollider2D>();
                    colliderSize_Origin = capsule.size;
                    break;
                case 1:
                    box = this.GetComponent<BoxCollider2D>();
                    colliderSize_Origin = box.size;
                    break;
                case 2:
                    circle = this.GetComponent<CircleCollider2D>();
                    colliderSize_Origin = new Vector2(circle.radius, circle.radius);
                    break;
            }
        }
        #endregion

        #region 碰撞检测

        /**
         * 实际使用时记得将系统中不必要的层碰撞关闭，提交性能
         */

        private void OnCollisionEnter2D(Collision2D collision)
        {
            int nLayer = collision.gameObject.layer;
            //Debug.Log("发生碰撞：" + nLayer);
            if (nLayer == SolidLayerValue || nLayer == PlatformLayerValue)
            {
                if (body.velocity.y <= 0.01)//速度为负数说明正在掉落而不是上升
                    CheckCollision(collision, nLayer);
            }
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            int nLayer = collision.gameObject.layer;
            if (nLayer == SolidLayerValue || nLayer == PlatformLayerValue)
            {
                CheckCollision(collision, nLayer);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            int nLayer = collision.gameObject.layer;
            if (nLayer == SolidLayerValue || nLayer == PlatformLayerValue)
            {
                onGround = false;
                onOneWay = false;
            }
        }

        /// <summary>
        /// 碰撞检测
        /// </summary>
        /// <param name="collision"></param>
        /// <param name="layerNumber"></param>
        private void CheckCollision(Collision2D collision, int layerNumber)
        {
            for (int i = 0; i < collision.contacts.Length; i++)
            {
                switch (layerNumber)
                {
                    case SolidLayerValue:
                        onGround |= collision.GetContact(i).normal.y >= 0.35f;
                        break;
                    case PlatformLayerValue:
                        onOneWay |= collision.GetContact(i).normal.y >= 0.35f;
                        break;
                }
            }

            if (onOneWay) onGround = onOneWay;
        }
        #endregion

        #region 属性
        public bool OnGround { get { return onGround; } }

        public bool OnOneWay { get { return onOneWay; } }

        public LayerMask SolidMask { get { return solidMask; } }

        public LayerMask OnewayMask { get { return oneWayMask; } }

        public LayerMask LayerMask { get { return layerMask; } }
        #endregion

    }
}