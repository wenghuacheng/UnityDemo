using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AI.Goal
{
    /// <summary>
    /// 目标
    /// </summary>
    public class AgentGoal
    {
        private AgentGoal(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        /// <summary>
        /// 优先级
        /// </summary>
        public float Priority { get; private set; }

        /// <summary>
        /// 完成目标时的匹配条件
        /// </summary>
        public HashSet<AgentBelief> DesiredEffects { get; } = new();

        #region Builder
        /// <summary>
        /// 构造模式生成AgentGoal
        /// </summary>
        public class Builder
        {
            readonly AgentGoal goal;

            public Builder(string name)
            {
                goal = new AgentGoal(name);
            }

            public Builder WithPriority(float priority)
            {
                goal.Priority = priority;
                return this;
            }

            public Builder WithDesiredEffect(AgentBelief effect)
            {
                goal.DesiredEffects.Add(effect);
                return this;
            }

            public AgentGoal Build()
            {
                return goal;
            }
        }
        #endregion
    }
}
