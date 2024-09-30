using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public class SkeletonEnemyBattleState : SkeletonEnemyState
    {
        private const string AnimatorName = "Move";
        private Transform _target = null;
        private RaycastHit2D _targetHit;
        private float speed = 1.5f;//追赶速度
        private float prevAttackTime = 0;//前一个攻击时的时间

        public SkeletonEnemyBattleState(SkeletonEnemy enemy, SkeletonStateMachine stateMachine) : base(enemy, stateMachine, AnimatorName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            RefreshTarget();
        }

        public override void Update()
        {
            base.Update();
            FaceToTarget();
            RefreshTarget();

            //到达攻击距离时则不再移动
            if (_targetHit.distance > enemy.AttackTargetDistance)
                enemy.Rb.velocity = new Vector2(enemy.transform.right.x * speed, enemy.Rb.velocity.y);
        }

        public override bool TransitionState()
        {
            if (_target == null)
            {
                stateMachine.ChangeState(SkeletonStateMachine.SkeletonStateEnum.Idle);
                return true;
            }
            else if (_targetHit.distance <= enemy.AttackTargetDistance && CanAttack())
            {
                prevAttackTime = Time.time;
                stateMachine.ChangeState(SkeletonStateMachine.SkeletonStateEnum.Attack);
                return true;
            }
            else if (this.costTime <= 0 && _target == null)
            {
                //目标离开视野
                stateMachine.ChangeState(SkeletonStateMachine.SkeletonStateEnum.Idle);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 面向目标
        /// </summary>
        private void FaceToTarget()
        {
            if (_target == null) return;

            var direction = _target.transform.position - enemy.transform.position;

            if (direction.x < 0 && enemy.Right.x > 0)
                enemy.Flip();
            else if (direction.x > 0 && enemy.Right.x < 0)
                enemy.Flip();
        }

        /// <summary>
        /// 是否可以攻击
        /// </summary>
        private bool CanAttack()
        {
            return true;
            //return (Time.time - prevAttackTime) > enemy.AttackCoolDown;
        }

        /// <summary>
        /// 刷新目标
        /// </summary>
        private void RefreshTarget()
        {
            _targetHit = enemy.GetTarget();
            _target = _targetHit.transform;
        }
    }
}