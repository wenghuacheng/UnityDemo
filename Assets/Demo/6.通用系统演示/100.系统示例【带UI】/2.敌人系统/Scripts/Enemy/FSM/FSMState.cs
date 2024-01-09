using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 单个状态
    /// 执行具体逻辑与状态切换
    /// </summary>
    [Serializable]
    public class FSMState
    {
        public string ID;

        public FSMAction[] Actions;//当前状态的执行操作【可以边攻击，边追击】
        public FSMTransition[] Transitions;//当前状态切换的其他状态时的判断条件


        public void UpdateState(EnemyBrain enmeyBrain)
        {
            ExecuteActions();
            ExecuteTransitions(enmeyBrain);
        }

        /// <summary>
        /// 执行当前状态的所有的逻辑
        /// </summary>
        private void ExecuteActions()
        {
            for (int i = 0; i < Actions.Length; i++)
            {
                Actions[i].Act();
            }
        }

        /// <summary>
        /// 执行状态切换的判断
        /// </summary>
        private void ExecuteTransitions(EnemyBrain enmeyBrain)
        {
            if (Transitions == null || Transitions.Length <= 0) return;

            for (int i = 0; i < Transitions.Length; i++)
            {
                var transition = Transitions[i];
                var value = transition.Decision.Decide();
                if (value)
                {
                    enmeyBrain.ChangeState(transition.TrueState);
                }
                else
                {
                    enmeyBrain.ChangeState(transition.FalseState);
                }
            }
        }
    }
}
