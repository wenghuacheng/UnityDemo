using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AI.PathFind.GraphWay
{
    /// <summary>
    /// 节点类型
    /// 当前节点是通路还是障碍
    /// </summary>
    public enum NodeType { Open, Block };

    /// <summary>
    /// 节点对象
    /// </summary>
    public class Node
    {
        public NodeType nodeType = NodeType.Open;

        public int xIndex = -1;
        public int yIndex = -1;

        public List<Node> neighbors = new List<Node>();
        public Node previous = null;

        public Vector3 position;

        public Node(int xIndex, int yIndex, NodeType nodeType)
        {
            this.xIndex = xIndex;
            this.yIndex = yIndex;
            this.nodeType = nodeType;
        }

        public void Reset()
        {
            previous = null;
        }
    }
}