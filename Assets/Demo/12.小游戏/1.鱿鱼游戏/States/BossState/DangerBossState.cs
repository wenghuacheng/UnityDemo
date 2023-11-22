using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Demo.Games.SquidGame.States.BossStateMachine;

namespace Demo.Games.SquidGame.States
{
    /// <summary>
    /// Î£ÏÕ×´Ì¬
    /// </summary>
    public class DangerBossState : BaseBossState
    {
        public DangerBossState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
        {
            this.color = Color.red;
        }

        public override void Enter()
        {
            base.Enter();
            EventSetter.RaiseBossStateChanged(BossStateEnum.Danger);
            this.time = Random.Range(2f, 5);
        }

        public override bool TransitionState()
        {
            if (time <= 0)
            {
                stateMachine.ChangeState(BossStateMachine.BossStateEnum.Safe);
                return true;
            }
            return false;
        }
    }
}