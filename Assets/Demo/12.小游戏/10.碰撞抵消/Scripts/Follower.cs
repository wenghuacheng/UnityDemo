using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Games.CollisionEliminate
{
    /// <summary>
    /// 跟随者
    /// </summary>
    public abstract class Follower : MonoBehaviour
    {
        protected Rigidbody2D _rb;
        protected float _speed = 5;
        public Transform _target;

        public event Action<Follower> OnDestory;

        protected virtual void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        protected virtual void Update()
        {
            MoveToTarget();
        }

        /// <summary>
        /// 设置目标
        /// </summary>
        public void SetTarget(Transform target)
        {
            this._target = target;
        }

        /// <summary>
        /// 向目标移动
        /// </summary>
        private void MoveToTarget()
        {
            if (this._target == null) return;

            var direction = (_target.position - this.transform.position).normalized;
            this._rb.velocity = direction * _speed;
        }

        /// <summary>
        /// 触发销毁事件
        /// </summary>
        public void RaiseFollowerDestory()
        {
            OnDestory?.Invoke(this);
        }
    }
}