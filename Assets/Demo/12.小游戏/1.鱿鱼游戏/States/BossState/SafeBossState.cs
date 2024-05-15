using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Demo.Games.SquidGame.States.BossStateMachine;

namespace Demo.Games.SquidGame.States
{
    /// <summary>
    /// °²È«×´Ì¬
    /// </summary>
    public class SafeBossState : BaseBossState
    {
        public SafeBossState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
        {
            this.color = Color.green;
        }

        public override void Enter()
        {
            base.Enter();
            EventSetter.RaiseBossStateChanged(BossStateEnum.Safe);
            this.time = Random.Range(3, 10);
        }

        public override bool TransitionState()
        {
            if (time <= 0)
            {
                stateMachine.ChangeState(BossStateMachine.BossStateEnum.Prepare);
                return true;
            }
            return false;
        }
    }
}