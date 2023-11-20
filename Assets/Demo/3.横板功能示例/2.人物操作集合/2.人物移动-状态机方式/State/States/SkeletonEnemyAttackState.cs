using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public class SkeletonEnemyAttackState : SkeletonEnemyState
    {
        private const string AnimatorName = "Attack";

        public SkeletonEnemyAttackState(SkeletonEnemy enemy, SkeletonStateMachine stateMachine) : base(enemy, stateMachine, AnimatorName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            this.enemy.Rb.velocity = new Vector2(0, 0);
        }

        public override bool TransitionState()
        {
            if (animatorTriggerFlag)
            {
                stateMachine.ChangeState(SkeletonStateMachine.SkeletonStateEnum.Battle);
                return true;
            }
            return false;
        }
    }
}