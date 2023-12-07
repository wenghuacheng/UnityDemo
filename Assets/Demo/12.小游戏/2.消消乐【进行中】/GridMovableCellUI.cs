using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.XXL
{
    /// <summary>
    /// 可移动的单元格UI【用于消除后下坠】
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
        /// 设置目标位置
        /// </summary>
        /// <param name="targetPosition"></param>
        public void SetTarget(Vector2 targetPosition)
        {
            this._targetPosition = targetPosition;
            _direction = this._targetPosition - this.transform.position;
            isReachTarget = false;
        }

        /// <summary>
        /// 到达位置
        /// </summary>
        public bool MatchTarget()
        {
            if (Vector2.Distance(this.transform.position, this._targetPosition) < 0.1f)
            {
                //todo：给指定的位置发送命令
                isReachTarget = true;
                return true;
            }
            return false;
        }
    }
}
