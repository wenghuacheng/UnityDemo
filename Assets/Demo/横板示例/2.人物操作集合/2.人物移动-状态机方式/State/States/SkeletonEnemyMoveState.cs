using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public class SkeletonEnemyMoveState : SkeletonEnemyGroundState
    {
        private const string AnimatorName = "Move";
        private float speed = 1f;

        public SkeletonEnemyMoveState(SkeletonEnemy enemy, SkeletonStateMachine stateMachine) : base(enemy, stateMachine, AnimatorName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            enemy.Rb.velocity = new Vector2(enemy.transform.right.x * speed, enemy.Rb.velocity.y);
        }

        public override void Exit()
        {
            base.Exit();
            enemy.Rb.velocity = new Vector2(0, enemy.Rb.velocity.y);
        }

        public override bool TransitionState()
        {
            if (base.TransitionState()) return true;

            if (enemy.IsWallDetected())
            {
                stateMachine.ChangeState(SkeletonStateMachine.SkeletonStateEnum.Idle);
                return true;
            }
            return false;
        }
    }
}