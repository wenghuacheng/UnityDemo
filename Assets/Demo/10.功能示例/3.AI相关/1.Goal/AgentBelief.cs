using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AI.Goal
{
    /// <summary>
    /// �뷨
    /// </summary>
    public class AgentBelief
    {
        private AgentBelief(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        //��������
        Func<bool> condition = () => false;

        //Ŀ��λ��׷��
        Func<Vector3> observedLocation = () => Vector3.zero;

        //Ŀ��λ��
        public Vector3 Location()
        {
            return observedLocation();
        }

        /// <summary>
        /// �����Ƿ���������
        /// </summary>
        /// <returns></returns>
        public bool Evaluate()
        {
            return condition();
        }

        #region Builder
        /// <summary>
        /// ����ģʽ����AgentBelief
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