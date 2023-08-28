using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public class SkeletonEnemyGroundState : SkeletonEnemyState
    {
        public SkeletonEnemyGroundState(SkeletonEnemy enemy, SkeletonStateMachine stateMachine, string animatorName) : base(enemy, stateMachine, animatorName)
        {
        }

        public override bool TransitionState()
        {
            if (enemy.GetTarget() != null)
            {
                stateMachine.ChangeState(SkeletonStateMachine.SkeletonStateEnum.Battle);
            }
            return false;
        }
    }
}
