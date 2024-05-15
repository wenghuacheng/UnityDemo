using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.MetroidvaniaGame
{
    /// <summary>
    /// �����ж���
    /// ע�⣺GroundSensor�����layer�������趨Ϊ��Player����ײ
    /// </summary>
    public class GroundSensor : MonoBehaviour
    {
        //layer��ʹ�õ�ͼ�㣬����ʹ�õ���Ground=6��Platform=9���ֱ��������ƽ̨
        public const int SolidLayerValue = 6;
        public const int PlatformLayerValue = 9;

        //��ײ����  0��Capsule  1��box  2��circle
        [SerializeField] private int collisionType;

        private Vector2 colliderSize_Origin;//ԭʼ��ײ��Χ
        private CapsuleCollider2D capsule;//��ײ����=0ʱʹ�õ���ײ��
        private BoxCollider2D box;//��ײ����=1ʱʹ�õ���ײ��
        private CircleCollider2D circle;//��ײ����=2ʱʹ�õ���ײ��

        private Rigidbody2D body;

        [SerializeField] private bool onGround;//�Ƿ��ڵذ���
        [SerializeField] private bool onOneWay;//�Ƿ���ƽ̨��

        private LayerMask solidMask = 1 << SolidLayerValue;//�ذ�ͼ��
        private LayerMask oneWayMask = 1 << PlatformLayerValue;//ƽ̨ͼ��(��̨)
        private LayerMask layerMask = 1 << SolidLayerValue | 1 << PlatformLayerValue;//�ذ�ͼ��&ƽ̨ͼ��

        #region ��ʼ��
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

        #region ��ײ���

        /**
         * ʵ��ʹ��ʱ�ǵý�ϵͳ�в���Ҫ�Ĳ���ײ�رգ��ύ����
         */

        private void OnCollisionEnter2D(Collision2D collision)
        {
            int nLayer = collision.gameObject.layer;
            //Debug.Log("������ײ��" + nLayer);
            if (nLayer == SolidLayerValue || nLayer == PlatformLayerValue)
            {
                if (body.velocity.y <= 0.01)//�ٶ�Ϊ����˵�����ڵ������������
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
        /// ��ײ���
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

        #region ����
        public bool OnGround { get { return onGround; } }

        public bool OnOneWay { get { return onOneWay; } }

        public LayerMask SolidMask { get { return solidMask; } }

        public LayerMask OnewayMask { get { return oneWayMask; } }

        public LayerMask LayerMask { get { return layerMask; } }
        #endregion

    }
}