using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Demo.Games.GoGrid
{
    public class Player : MonoBehaviour
    {
        [SerializeField] protected Map map;

        private int curIndex = 0;
        private int targetIndex = 0;
        private float speed = 6;

        private bool IsArrivedTarget = true;

        public event Action OnMovementEnd;

        //是否已经到达终点
        public bool IsArrivedEnd { get; private set; } = false;


        protected virtual void Start()
        {
            this.transform.position = map.CellPositionList.FirstOrDefault();
        }

        protected virtual void Update()
        {
            Movement();
        }

        /// <summary>
        /// 设置Roll值
        /// </summary>
        /// <param name=""></param>
        public void SetRoll(int count)
        {
            targetIndex = curIndex + count;
            if (targetIndex >= map.CellPositionList.Count)
                targetIndex = map.CellPositionList.Count - 1;

            IsArrivedTarget = false;
        }

        /// <summary>
        /// 移动
        /// </summary>
        private void Movement()
        {
            if (IsArrivedTarget) return;

            var pos = map.CellPositionList[curIndex];
            if (Vector2.Distance(this.transform.position, pos) <= 0.1f)
            {
                if (curIndex >= map.CellPositionList.Count - 1)
                {
                    Debug.Log("到达终点");
                    IsArrivedEnd = true;
                }

                if (curIndex == targetIndex)
                {
                    Debug.Log("到达位置");
                    IsArrivedTarget = true;
                    OnMovementEnd?.Invoke();
                }
                else
                {
                    curIndex++;
                }
            }
            else
            {
                var direction = (pos - this.transform.position).normalized;
                this.transform.Translate(direction * speed * Time.deltaTime);
            }
        }

    }
}