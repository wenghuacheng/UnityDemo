using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ObjectMove.FormationMovement
{
    /// <summary>
    /// 队列中单个元素
    /// </summary>
    public class SingleItem : MonoBehaviour
    {
        private float _speed;
        public Vector3 _target;
        private bool isMatchTarget = true;//是否达到位置

        private float showLineTime = 0;

        private Rigidbody2D _rb;
        private LineRenderer _lineRenderer;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _lineRenderer = GetComponent<LineRenderer>();
            this._target = this.transform.position;
        }

        void Update()
        {
            Movement();
            ShowLine();
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Movement()
        {
            if (isMatchTarget) return;
            _rb.velocity = (this._target - this.transform.position).normalized * _speed;

            if (Vector2.Distance(_target, this.transform.position) <= 0.05f)
            {
                isMatchTarget = true;
                _rb.velocity = Vector2.zero;
            }
        }

        /// <summary>
        /// 设置目标
        /// </summary>
        public void SetTarget(float speed, Vector2 target)
        {
            this._target = target;
            this._speed = speed;
            this.isMatchTarget = false;

            //设置显示连线时间
            showLineTime = 2;
        }

        /// <summary>
        /// 显示目标连线
        /// </summary>
        /// <returns></returns>
        private void ShowLine()
        {
            showLineTime -= Time.deltaTime;
            if (showLineTime > 0)
            {
                _lineRenderer.positionCount = 2;
                _lineRenderer.SetPositions(new Vector3[] { this.transform.position, this._target });
            }
            else
            {
                _lineRenderer.positionCount = 0;
                _lineRenderer.SetPositions(new Vector3[0]);
            }
        }
    }
}