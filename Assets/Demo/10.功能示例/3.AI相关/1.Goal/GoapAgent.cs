using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.AI.Goal
{
    /// <summary>
    /// AI系统
    /// </summary>
    public class GoapAgent : MonoBehaviour
    {
        [Header("探测器")]
        [SerializeField] private Sensor chaseSensor, attackSensor;

        [Header("已知位置")]
        [SerializeField] private Transform restingPosition;//休息点
        [SerializeField] private Transform foodShack;//食物点
        [SerializeField] private Transform doorOnePosition;
        [SerializeField] private Transform doorTwoPosition;

        [Header("人物状态")]
        public float Health = 100;
        public float Stamina = 100;

        private Rigidbody2D rb;
        private GameObject target;
        private Vector3 destination;
        private AgentGoal lastGoal;//默认目标

        public AgentGoal currentGoal;//当前目标
        public ActionPlan actionPlan;
        public AgentAction currentAction;//当前动作

        public Dictionary<string, AgentBelief> beliefs;
        public HashSet<AgentAction> actions;
        public HashSet<AgentGoal> goals;

        IGoapPlanner gPlanner;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();

            gPlanner = new GoapPlanner();
        }

        private void Start()
        {
            //SetupTimers();
            StartCoroutine(SetupTimers());
            SetupBeliefs();
            SetupActions();
            SetupGoals();
        }

        private void OnEnable()
        {
            chaseSensor.OnTargetChanged += HandleChaseTargetChanged;
        }
        private void OnDisable()
        {
            chaseSensor.OnTargetChanged -= HandleChaseTargetChanged;
        }

        private void Update()
        {
            if (currentAction == null)
            {
                Debug.Log("产生新的计划");
                CalculatePlan();

                if (actionPlan != null && actionPlan.Actions.Count > 0)
                {
                    currentGoal = actionPlan.AgentGoal;
                    currentAction = actionPlan.Actions.Pop();
                    currentAction.Start();

                    Debug.Log("当前任务改变为...");
                }
            }

            if (actionPlan != null && currentAction != null)
            {
                currentAction.Update(Time.deltaTime);
                if (currentAction.Complete)
                {
                    Debug.Log($"{currentAction.Name}已完成");
                    currentAction.Stop();
                    currentAction = null;

                    if (actionPlan.Actions.Count == 0)
                    {
                        Debug.Log("计划完成");
                        lastGoal = currentGoal;
                        currentGoal = null;
                    }
                }
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator SetupTimers()
        {
            while (true)
            {
                UpdateStats();
                yield return new WaitForSeconds(2f);
            }
        }

        private void SetupBeliefs()
        {
            beliefs = new Dictionary<string, AgentBelief>();

            BeliefFactory factory = new BeliefFactory(this, beliefs);

            factory.AddBelief("Nothing", () => false);

            //todo:设置检测条件（两个条件互斥）
            factory.AddBelief("AgentIdle", () => false);
            factory.AddBelief("AgentMove", () => false);
        }

        private void SetupActions()
        {
            actions = new HashSet<AgentAction>();

            actions.Add(new AgentAction.Builder("Relax")
                .WithStrategy(new IdleStratepy(5))
                .AddEffect(beliefs["Nothing"])
                .Build());

            actions.Add(new AgentAction.Builder("Relax")
              .WithStrategy(new WanderStratepy(this.transform, 10))
              .AddEffect(beliefs["Nothing"])
              .Build());
        }

        private void SetupGoals()
        {
            goals = new HashSet<AgentGoal>();

            goals.Add(new AgentGoal.Builder("Chill Out")
                .WithPriority(1)
                .WithDesiredEffect(beliefs["Nothing"])
                .Build());

            goals.Add(new AgentGoal.Builder("Wander")
              .WithPriority(1)
              .WithDesiredEffect(beliefs["AgentMove"])
              .Build());
        }

        /// <summary>
        /// 更新角色状态
        /// </summary>
        private void UpdateStats()
        {
            Stamina += InRangeOf(restingPosition.position, 3f) ? 20 : -10;//靠近休息点加体力，否则减体力
            Health += InRangeOf(foodShack.position, 3f) ? 20 : -5;//靠近食物点点加生命，否则减生命
            Stamina = Mathf.Clamp(Stamina, 0, 100);
            Health = Mathf.Clamp(Health, 0, 100);
        }

        private void HandleChaseTargetChanged()
        {
            Debug.Log("Target Changed,Clearing Current Action And Goal");

            currentAction = null;
            currentGoal = null;
        }

        /// <summary>
        /// 检测距离是否满足要求
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        bool InRangeOf(Vector3 pos, float range)
        {
            return Vector3.Distance(this.transform.position, pos) < range;
        }


        private void CalculatePlan()
        {
            var priorityLevel = currentGoal?.Priority ?? 0;

            HashSet<AgentGoal> goalsToCheck = goals;

            if (currentGoal != null)
            {
                goalsToCheck = new HashSet<AgentGoal>(goals.Where(g => g.Priority > priorityLevel));
            }

            var potentialPlan = gPlanner.Plan(this, goalsToCheck, lastGoal);
            if (potentialPlan != null)
            {
                actionPlan = potentialPlan;
            }
        }

    }
}
