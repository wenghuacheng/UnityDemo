using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.HB.Player.Platform
{
    public class KnockBack : MonoBehaviour
    {
        public Action OnKnockbackStart;
        public Action OnKnockbackEnd;

        [SerializeField] private float knockbackTime = 0.2f;

        private Vector3 _hitDirection;//�ƶ�����
        private float _knockbackThrust;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            OnKnockbackStart += ApplyKnockbackForce;
            OnKnockbackEnd += StopKnockRountine;
        }

        private void OnDisable()
        {
            OnKnockbackStart -= ApplyKnockbackForce;
            OnKnockbackEnd -= StopKnockRountine;
        }

        public void GetKnockedBack(Vector3 hitDirection, float knockbackThrust)
        {
            _hitDirection = hitDirection;
            _knockbackThrust = knockbackThrust;

            OnKnockbackStart?.Invoke();
        }

        private void ApplyKnockbackForce()
        {
            Vector3 difference = (transform.position - _hitDirection).normalized * _knockbackThrust * _rigidbody.mass;
            _rigidbody.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockRoutine());
        }

        private IEnumerator KnockRoutine()
        {
            yield return new WaitForSeconds(knockbackTime);
            OnKnockbackEnd?.Invoke();
        }

        private void StopKnockRountine()
        {
            _rigidbody.velocity = Vector2.zero;
        }


        #region Test
        private void OnGUI()
        {
            /**
             * ע���:����ʱ��������ǲ��ƶ�״̬������û��Ч��
             * ������Ҫ�ƶ�ģ�����һ��CanMove���Լ���OnKnockbackStart��OnKnockbackEnd������ʱ��������Ϊ�����ƶ����Ȼ��˺�ָ�
             * **/
            if (GUI.Button(new Rect(0, 0, 100, 30), "����"))
            {
                this.GetKnockedBack(Vector2.right, 20);
            }
        }

        #endregion
    }
}