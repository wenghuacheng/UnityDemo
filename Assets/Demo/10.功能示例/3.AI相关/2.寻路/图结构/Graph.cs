using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Demo.AI.PathFind.GraphWay
{
    /// <summary>
    /// 图表，负责显示
    /// </summary>
    public class Graph : MonoBehaviour
    {
        public Node[,] nodes;
        public List<Node> walls = new List<Node>();

        private int[,] m_mapData;
        private int m_width;
        private int m_height;

        //四周的八个方向向量
        public static readonly Vector2[] allDirections = {
            new Vector2(0f,1f),
            new Vector2(0f,-1f),
            new Vector2(1f,0f),
            new Vector2(-1f,0f),
            new Vector2(-1f,1f),
            new Vector2(1f,1f),
            new Vector2(-1f,-1f),
            new Vector2(1f,-1f),
        };

        public void Initialize(int[,] mapData)
        {
            m_mapData = mapData;
            m_width = mapData.GetLength(0);
            m_height = mapData.GetLength(1);

            nodes = new Node[m_width, m_height];

            //生成节点对象
            for (int y = 0; y < m_height; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    NodeType type = (NodeType)mapData[x, y];
                    Node newNode = new Node(x, y, type);
                    nodes[x, y] = newNode;

                    newNode.position = new Vector3(x, y,0);

                    if (type == NodeType.Block)
                    {
                        walls.Add(newNode);
                    }
                }
            }

            //绑定四周节点
            for (int y = 0; y < m_height; y++)
            {
                for (int x = 0; x < m_width; x++)
                {
                    if (nodes[x, y].nodeType != NodeType.Block)
                    {
                        nodes[x, y].neighbors = GetNeighbors(x, y);
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否在范围内
        /// </summary>
        public bool IsWithinBounds(int x, int y)
        {
            return x >= 0 && x < m_width && y >= 0 && y < m_height;
        }

        /// <summary>
        /// 获取周围节点
        /// </summary>
        /// <returns></returns>
        private List<Node> GetNeighbors(int x, int y, Node[,] nodes, Vector2[] directions)
        {
            List<Node> neighborNodes = new List<Node>();

            foreach (var dir in directions)
            {
                int newX = x + (int)dir.x;
                int newY = y + (int)dir.y;

                if (IsWithinBounds(newX, newY) && nodes[newX, newY] != null &&
                    nodes[newX, newY].nodeType != NodeType.Block)
                    neighborNodes.Add(nodes[newX, newY]);

            }

            return neighborNodes;
        }

        private List<Node> GetNeighbors(int x, int y)
        {
            return GetNeighbors(x, y, nodes, allDirections);
        }
    }
}