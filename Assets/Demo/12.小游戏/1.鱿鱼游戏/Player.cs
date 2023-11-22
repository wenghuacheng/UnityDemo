using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.SquidGame
{
    public class Player : MonoBehaviour
    {
        private float speed = 10;

        private bool isDanger = false;
        private bool isWin = false;
        private Vector2 standPosition;//站立位置

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
            var x = Input.GetAxis("Horizontal");
            if (x < 0)
            {
                this.transform.Translate(new Vector3(x * speed * Time.deltaTime, 0));
            }
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

        private void EventSetter_OnBossStateChanged(States.BossStateMachine.BossStateEnum state)
        {
            isDanger = state == States.BossStateMachine.BossStateEnum.Danger;
            if (isDanger)
                standPosition = this.transform.position;
        }

        private void OnDestroy()
        {
            EventSetter.OnBossStateChanged -= EventSetter_OnBossStateChanged;
        }

        public void Win()
        {
            isWin = true;
        }
    }
}