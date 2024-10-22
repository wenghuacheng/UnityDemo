using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.AI.Goal
{
    /// <summary>
    /// 执行策略
    /// </summary>
    public interface IActionStrategy
    {
        /// <summary>
        /// 是否可执行
        /// </summary>
        bool CanPerform { get; }

        /// <summary>
        /// 是否已完成
        /// </summary>
        bool Complete { get; }

        public void Start()
        {

        }

        public void Update(float deltaTime)
        {

        }

        public void Stop()
        {

        }

    }

    public class IdleStratepy : IActionStrategy
    {
        private float duration;
        private float runningTime;


        public IdleStratepy(float duration)
        {
            this.duration = duration;
            Complete = false;
        }


        public bool CanPerform => true;
        public bool Complete { get; private set; }

        public void Update(float deltaTime)
        {
            runningTime += deltaTime;
            if (runningTime > duration)
            {
                Complete = true;
            }
        }

    }

    public class WanderStratepy : IActionStrategy
    {
        private float wanderRadius;
        private Transform agent;

        List<Vector2> posList = new List<Vector2>();
        int currentPos = 0;

        public WanderStratepy(Transform agent, float wanderRadius)
        {
            this.wanderRadius = wanderRadius;
            this.agent = agent;
        }

        public bool CanPerform => !Complete;

        public bool Complete { get; private set; }

        public void Start()
        {
            Complete = false;
            for (int i = 0; i < 5; i++)
            {
                var randomDirection = UnityEngine.Random.insideUnitCircle * wanderRadius;
                //todo:随机移动(教程中使用NavMeshAgent移动)
                posList.Add(randomDirection);
            }

            //Complete = true;
        }

        public void Update(float deltaTime)
        {
            if (Vector2.Distance(posList[currentPos], agent.transform.position) < 0.1f)
            {
                currentPos++;
            }

            if (currentPos >= posList.Count - 1)
            {
                Complete = true;
                return;
            }

            agent.transform.position = Vector2.MoveTowards(agent.transform.position, posList[currentPos], deltaTime);
        }

    }
}
