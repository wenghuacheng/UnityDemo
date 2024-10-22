using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AI.Goal
{
    public class AgentAction
    {
        private IActionStrategy strategy;//执行策略

        private AgentAction(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        /// <summary>
        /// 执行成本
        /// </summary>
        public float Cost { get; private set; }

        /// <summary>
        /// 先决条件
        /// </summary>
        public HashSet<AgentBelief> Preconditions { get; } = new();

        /// <summary>
        /// 执行完毕后的影响
        /// </summary>
        public HashSet<AgentBelief> Effects { get; } = new();

        /// <summary>
        /// 是否已执行完成
        /// </summary>
        public bool Complete
        {
            get { return strategy.Complete; }
        }

        public void Start()
        {
            strategy.Start();
        }

        public void Update(float deltaTime)
        {
            if (strategy.CanPerform)
                strategy.Update(deltaTime);

            if (!strategy.Complete)
                return;

            //执行结束，改变后效
            foreach (var effect in Effects)
            {
                effect.Evaluate();
            }
        }

        public void Stop()
        {
            strategy.Stop();
        }

        #region Builder
        /// <summary>
        /// AgentAction
        /// </summary>
        public class Builder
        {
            readonly AgentAction action;

            public Builder(string name)
            {
                action = new AgentAction(name) { Cost = 1 };
            }

            public Builder WithCost(float cost)
            {
                action.Cost = cost;
                return this;
            }

            public Builder WithStrategy(IActionStrategy strategy)
            {
                action.strategy = strategy;
                return this;
            }

            public Builder AddPrecondition(AgentBelief precondition)
            {
                action.Preconditions.Add(precondition);
                return this;
            }

            public Builder AddEffect(AgentBelief effect)
            {
                action.Effects.Add(effect);
                return this;
            }

            public AgentAction Build()
            {
                return action;
            }
        }

        #endregion
    }
}
