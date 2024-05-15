using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.XXL
{
    /// <summary>
    /// ���ƶ��ĵ�Ԫ��UI��������������׹��
    /// </summary>
    public class GridMovableCellUI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer iconRenderer;

        private Vector3 _targetPosition;
        private Vector3 _direction;
        private float speed = 2;

        private bool isReachTarget = false;

        void Start()
        {

        }

        void Update()
        {
            if (!MatchTarget())
                this.transform.Translate(_direction * speed * Time.deltaTime);
        }

        /// <summary>
        /// ����Ŀ��λ��
        /// </summary>
        /// <param name="targetPosition"></param>
        public void SetTarget(Vector2 targetPosition)
        {
            this._targetPosition = targetPosition;
            _direction = this._targetPosition - this.transform.position;
            isReachTarget = false;
        }

        /// <summary>
        /// ����λ��
        /// </summary>
        public bool MatchTarget()
        {
            if (Vector2.Distance(this.transform.position, this._targetPosition) < 0.1f)
            {
                //todo����ָ����λ�÷�������
                isReachTarget = true;
                return true;
            }
            return false;
        }
    }
}
