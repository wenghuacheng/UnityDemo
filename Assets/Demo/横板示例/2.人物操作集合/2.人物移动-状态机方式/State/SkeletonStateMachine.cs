using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HB.Operation.State.SkeletonStateMachine;

namespace HB.Operation.State
{
    public class SkeletonStateMachine : IStateMachine<SkeletonStateEnum, SkeletonEnemyState>
    {
        public enum SkeletonStateEnum
        {
            Idle, Move, Battle, Attack
        }

        public SkeletonStateMachine(SkeletonEnemy enemy)
        {
            IdleState = new SkeletonEnemyIdleState(enemy, this);
            MoveState = new SkeletonEnemyMoveState(enemy, this);
            BattleState = new SkeletonEnemyBattleState(enemy, this);
            AttackState = new SkeletonEnemyAttackState(enemy, this);
        }

        public SkeletonEnemyIdleState IdleState { get; private set; }

        public SkeletonEnemyMoveState MoveState { get; private set; }

        public SkeletonEnemyBattleState BattleState { get; private set; }

        public SkeletonEnemyAttackState AttackState { get; private set; }


        public override void Initialize()
        {
            this.CurrentState = InternalChangeState(SkeletonStateEnum.Idle);
            CurrentState?.Enter();
        }

        protected override SkeletonEnemyState InternalChangeState(SkeletonStateEnum state)
        {
            switch (state)
            {
                case SkeletonStateEnum.Idle:
                    return this.IdleState;
                case SkeletonStateEnum.Move:
                    return this.MoveState;
                case SkeletonStateEnum.Attack:
                    return this.AttackState;
                case SkeletonStateEnum.Battle:
                    return this.BattleState;
                default:
                    return this.IdleState;
            }
        }
    }
}