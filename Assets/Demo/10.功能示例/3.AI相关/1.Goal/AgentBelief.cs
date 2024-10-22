using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AI.Goal
{
    /// <summary>
    /// 想法
    /// </summary>
    public class AgentBelief
    {
        private AgentBelief(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        //满足条件
        Func<bool> condition = () => false;

        //目标位置追踪
        Func<Vector3> observedLocation = () => Vector3.zero;

        //目标位置
        public Vector3 Location()
        {
            return observedLocation();
        }

        /// <summary>
        /// 评估是否满足条件
        /// </summary>
        /// <returns></returns>
        public bool Evaluate()
        {
            return condition();
        }

        #region Builder
        /// <summary>
        /// 构造模式生成AgentBelief
        /// </summary>
        public class Builder
        {
            readonly AgentBelief belief;

            public Builder(string name)
            {
                belief = new AgentBelief(name);
            }

            public Builder WithCondition(Func<bool> condition)
            {
                belief.condition = condition;
                return this;
            }

            public Builder WithLocation(Func<Vector3> observedLocation)
            {
                belief.observedLocation = observedLocation;
                return this;
            }

            public AgentBelief Build()
            {
                return belief;
            }
        }

        #endregion
    }
}