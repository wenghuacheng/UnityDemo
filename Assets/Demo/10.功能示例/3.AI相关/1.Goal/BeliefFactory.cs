using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.AI.Goal
{
    public class BeliefFactory
    {
        readonly GoapAgent agent;
        readonly Dictionary<string, AgentBelief> beliefs;

        public BeliefFactory(GoapAgent agent, Dictionary<string, AgentBelief> beliefs)
        {
            this.agent = agent;
            this.beliefs = beliefs;
        }

        public void AddBelief(string key, Func<bool> condition)
        {
            beliefs.Add(key, new AgentBelief.Builder(key)
                .WithCondition(condition)
                .Build());
        }

        public void AddSensorBelief(string key, Sensor sensor)
        {
            beliefs.Add(key, new AgentBelief.Builder(key)
                .WithCondition(() => sensor.IsTargetInRange)
                .WithLocation(() => sensor.TargetPosition)
              .Build());
        }

        public void AddLocationBelief(string key, float distance, Transform locationCondition)
        {
            AddLocationBelief(key, distance, locationCondition.position);
        }

        public void AddLocationBelief(string key, float distance, Vector3 locationCondition)
        {
            beliefs.Add(key, new AgentBelief.Builder(key)
                .WithCondition(() => InRangeOf(locationCondition, distance))
                .WithLocation(() => locationCondition)
                .Build());
        }

        /// <summary>
        /// 检测距离是否满足要求
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        bool InRangeOf(Vector3 pos, float range)
        {
            return Vector3.Distance(agent.transform.position, pos) < range;
        }
    }
}
