using Demo.Games.SquidGame.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.SquidGame.States
{
    public abstract class BaseBossState : IState
    {
        protected Boss boss;
        protected BossStateMachine stateMachine;
        protected Color color;

        protected float time;

        public BaseBossState(Boss boss, BossStateMachine stateMachine)
        {
            this.boss = boss;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
            boss.SetColor(color);
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            time -= Time.deltaTime;
            TransitionState();
        }

        public abstract bool TransitionState();
    }
}