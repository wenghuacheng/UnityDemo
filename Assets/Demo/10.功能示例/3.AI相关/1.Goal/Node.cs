using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.AI.Goal
{
    public class Node
    {
        public Node(Node parent, AgentAction action, HashSet<AgentBelief> requiredEffects, float cost)
        {
            Parent = parent;
            Action = action;
            RequiredEffects = requiredEffects;
            Cost = cost;
        }

        public Node Parent { get; }
        public AgentAction Action { get; }
        public HashSet<AgentBelief> RequiredEffects { get; }
        public List<Node> Leaves { get; }
        public float Cost { get; }

        public bool IsLeafDead
        {
            get
            {
                return Leaves.Count == 0 && Action == null;
            }
        }
    }
}
