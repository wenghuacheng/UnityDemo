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

        private Vector3 _hitDirection;//移动方向
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
             * 注意点:击退时物体必须是不移动状态，否则没有效果
             * 所以需要移动模块添加一个CanMove属性监听OnKnockbackStart和OnKnockbackEnd，触发时将其设置为不可移动，等击退后恢复
             * **/
            if (GUI.Button(new Rect(0, 0, 100, 30), "击退"))
            {
                this.GetKnockedBack(Vector2.right, 20);
            }
        }

        #endregion
    }
}