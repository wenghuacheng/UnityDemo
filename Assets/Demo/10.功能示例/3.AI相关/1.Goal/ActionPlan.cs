using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.AI.Goal
{
    public interface IGoapPlanner
    {
        ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null);
    }

    public class GoapPlanner : IGoapPlanner
    {
        public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null)
        {
            //将目标基于优先级排序(需去掉所有条件已经满足的目标)
            var orderedGoals = goals.Where(g => g.DesiredEffects.Any(b => !b.Evaluate()))
                .OrderByDescending(g => g == mostRecentGoal ? g.Priority - 0.01 : g.Priority)//防止一直处理最近执行的那个任务，将其优先级稍稍降低
                .ToList();

            foreach (var goal in orderedGoals)
            {
                Node goalNode = new Node(null, null, goal.DesiredEffects, 0);

                //查询可以到达目标的路径
                if (FindPath(goalNode, agent.actions))
                {
                    if (goalNode.IsLeafDead) continue;

                    Stack<AgentAction> actionStack = new Stack<AgentAction>();
                    while (goalNode.Leaves.Count > 0)
                    {
                        var cheapestLeaf = goalNode.Leaves.OrderBy(p => p.Cost).First();
                        goalNode = cheapestLeaf;
                        actionStack.Push(cheapestLeaf.Action);
                    }

                    return new ActionPlan(goal, actionStack, goalNode.Cost);
                }
            }

            Debug.LogWarning("No Plan Found");
            return null;
        }

        private bool FindPath(Node parent, HashSet<AgentAction> actions)
        {
            foreach (var action in actions)
            {
                var requiredEffects = parent.RequiredEffects;

                //去掉已经完成的
                requiredEffects.RemoveWhere(b => b.Evaluate());

                //目标都完成了，创建计划
                if (!requiredEffects.Any())
                    return true;

                if (action.Effects.Any(requiredEffects.Contains))//???这什么写法
                {
                    //todo:需要理解
                    var newRequiredEffects = new HashSet<AgentBelief>(requiredEffects);
                    newRequiredEffects.ExceptWith(action.Effects);
                    newRequiredEffects.UnionWith(action.Preconditions);

                    var newAvailableActions = new HashSet<AgentAction>(actions);
                    newAvailableActions.Remove(action);

                    var newNode = new Node(parent, action, newRequiredEffects, parent.Cost + action.Cost);

                    //递归
                    if (FindPath(newNode, newAvailableActions))
                    {
                        parent.Leaves.Add(newNode);
                        newRequiredEffects.ExceptWith(newNode.Action.Preconditions);
                    }

                    //已经为所有目标已经被处理
                    if (newRequiredEffects.Count <= 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public class ActionPlan
    {
        public ActionPlan(AgentGoal agentGoal, Stack<AgentAction> actions, float totalCost)
        {
            AgentGoal = agentGoal;
            Actions = actions;
            TotalCost = totalCost;
        }

        //目标
        public AgentGoal AgentGoal { get; }
        //执行步骤
        public Stack<AgentAction> Actions { get; }
        //执行花费
        public float TotalCost { get; set; }


    }
}
