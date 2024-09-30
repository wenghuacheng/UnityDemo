using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ObjectMove.FormationMovement
{
    /// <summary>
    /// �����е���Ԫ��
    /// </summary>
    public class SingleItem : MonoBehaviour
    {
        private float _speed;
        public Vector3 _target;
        private bool isMatchTarget = true;//�Ƿ�ﵽλ��

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
        /// �ƶ�
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
        /// ����Ŀ��
        /// </summary>
        public void SetTarget(float speed, Vector2 target)
        {
            this._target = target;
            this._speed = speed;
            this.isMatchTarget = false;

            //������ʾ����ʱ��
            showLineTime = 2;
        }

        /// <summary>
        /// ��ʾĿ������
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