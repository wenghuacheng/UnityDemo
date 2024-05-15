using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.HideRaycast
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        private float RotationSpeed = 30;
        private float distance = 20;

        private Transform lockPlayer;
        private float lockRemainTime;
        private float maxLockTime = 1f;

        void Start()
        {
            //�����ѯ����ʼ������ײ���ڲ�����ô����ײ�������ᱻ������Ϊ��ѯ���
            Physics2D.queriesStartInColliders = false;
        }

        void Update()
        {
            AutoRotation();
            CheckRay();
        }

        /// <summary>
        /// �Զ���ת
        /// </summary>
        private void AutoRotation()
        {
            if (lockPlayer == null)
                this.transform.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
            else
            {
                //�������
                var vectorToTarget = (lockPlayer.transform.position - this.transform.position).normalized;
                this.transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
            }
        }

        /// <summary>
        /// ���߼��
        /// </summary>
        private void CheckRay()
        {
            lockRemainTime -= Time.deltaTime;
            if (lockPlayer != null && lockRemainTime <= 0)
            {
                lockPlayer = null;
            }

            var hit2D = Physics2D.Raycast(this.transform.position, this.transform.up, distance);
            if (hit2D.collider != null)
            {
                SetLine(hit2D.point);
                if (hit2D.collider.tag == "Player")
                {
                    //�������
                    lockPlayer = hit2D.transform;
                    lockRemainTime = maxLockTime;
                    Debug.Log("��ұ�������");
                }
            }
            else
            {
                Vector2 target = Vector2.zero;
                if (lockPlayer != null && lockRemainTime > 0)
                {
                    //�Ѿ�������Ŀ��
                    target = lockPlayer.transform.position;
                }
                else
                {
                    //δ������Ŀ��
                    target = this.transform.position + this.transform.up * distance;
                }

                SetLine(target);
            }
        }

        /// <summary>
        /// ������Ұ��
        /// </summary>
        private void SetLine(Vector2 target)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, target);
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawLine(this.transform.position, this.transform.up * distance);
        }
    }
}