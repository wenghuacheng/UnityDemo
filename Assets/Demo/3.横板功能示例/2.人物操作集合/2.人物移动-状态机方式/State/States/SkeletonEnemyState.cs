using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public abstract class SkeletonEnemyState : EnemyState
    {
        private string _animatorName;

        protected SkeletonStateMachine stateMachine;
        protected SkeletonEnemy enemy;

        public SkeletonEnemyState(SkeletonEnemy enemy, SkeletonStateMachine stateMachine, string animatorName)
        {
            this._animatorName = animatorName;
            this.stateMachine = stateMachine;
            this.enemy = enemy;
        }

        public override void Enter()
        {
            Debug.Log(this.GetType().Name);
            base.Enter();
            enemy.Animator.SetBool(_animatorName, true);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            enemy.Animator.SetBool(_animatorName, false);
        }

    }
}