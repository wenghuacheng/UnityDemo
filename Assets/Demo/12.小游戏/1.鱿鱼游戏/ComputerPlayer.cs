using Demo.Games.SquidGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.SquidGame
{
    /// <summary>
    /// 电脑玩家
    /// </summary>
    public class ComputerPlayer : MonoBehaviour
    {
        private float speed = 3;

        private bool isDanger = false;
        private Vector2 standPosition;//站立位置

        private bool canMove = false;
        private bool isWin = false;

        private void Start()
        {
            EventSetter.OnBossStateChanged += EventSetter_OnBossStateChanged;
        }

        private void Update()
        {
            if (isWin) return;

            Movement();
            CheckMovement();
        }

        private void Movement()
        {
            if (canMove)
                this.transform.Translate(new Vector3(-speed * Time.deltaTime, 0));
        }

        private void CheckMovement()
        {
            if (!isDanger) return;

            var cur = this.transform.position;
            if (cur.x != standPosition.x || cur.y != standPosition.y)
            {
                //移动了，死亡
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Boss状态改变处理
        /// </summary>
        /// <param name="state"></param>
        private void EventSetter_OnBossStateChanged(States.BossStateMachine.BossStateEnum state)
        {
            isDanger = state == States.BossStateMachine.BossStateEnum.Danger;
            if (isDanger)
                standPosition = this.transform.position;

            if (!isDanger)
                canMove = true;

            if (state == States.BossStateMachine.BossStateEnum.Safe)
            {
                //速度不一样,不然玩家打不过
                speed = Random.Range(0.5f, 2);
                Invoke("StartMove", Random.Range(0.1f, 0.5f));
            }
            else if (state == States.BossStateMachine.BossStateEnum.Prepare)
            {
                //随机停止时间
                Invoke("StopMove", Random.Range(0, 1f));
            }
        }

        private void StopMove()
        {
            canMove = false;
        }

        private void StartMove()
        {
            canMove = true;
        }

        public void Win()
        {
            isWin = true;
            canMove = false;
        }

        private void OnDestroy()
        {
            EventSetter.OnBossStateChanged -= EventSetter_OnBossStateChanged;
        }
    }
}