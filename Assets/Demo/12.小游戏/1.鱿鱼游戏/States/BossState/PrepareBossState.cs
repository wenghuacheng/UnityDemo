using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Demo.Games.SquidGame.States.BossStateMachine;

namespace Demo.Games.SquidGame.States
{
    /// <summary>
    /// ԤΣ��״̬
    /// </summary>
    public class PrepareBossState : BaseBossState
    {
        public PrepareBossState(Boss boss, BossStateMachine stateMachine) : base(boss, stateMachine)
        {
            this.color = Color.yellow;
        }

        public override void Enter()
        {
            base.Enter();
            EventSetter.RaiseBossStateChanged(BossStateEnum.Prepare);
            this.time = Random.Range(0.5f, 1.5f);
        }

        public override bool TransitionState()
        {
            if (time <= 0)
            {
                stateMachine.ChangeState(BossStateMachine.BossStateEnum.Danger);
                return true;
            }
            return false;
        }
    }
}