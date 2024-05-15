using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public class SkeletonEnemyIdleState : SkeletonEnemyGroundState
    {
        private const string AnimatorName = "Idle";
        private const float IdleTime = 2f;

        public SkeletonEnemyIdleState(SkeletonEnemy enemy, SkeletonStateMachine stateMachine) : base(enemy, stateMachine, AnimatorName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            costTime = IdleTime;
        }

        public override void Exit()
        {
            base.Exit();

            if (enemy.IsWallDetected())
            {
                //��⵽ǽ��ת��
                enemy.Flip();
            }
        }

        public override bool TransitionState()
        {
            if (base.TransitionState()) return true;

            if (costTime <= 0)
            {
                //�л�Ϊ����
                stateMachine.ChangeState(SkeletonStateMachine.SkeletonStateEnum.Move);
                return true;
            }
            return false;
        }
    }
}