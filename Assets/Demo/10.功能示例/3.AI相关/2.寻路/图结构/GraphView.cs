using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AI.PathFind.GraphWay
{
    [RequireComponent(typeof(Graph))]
    public class GraphView : MonoBehaviour
    {
        public GameObject nodeViewPrefab;

        public Color baseColor = Color.white;
        public Color wallColor = Color.black;

        public void Init(Graph graph)
        {
            foreach (var node in graph.nodes)
            {
                GameObject tile = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity, this.transform);
                NodeView nodeView = tile.GetComponent<NodeView>();

                if (nodeView != null)
                {
                    nodeView.Init(node);
                    nodeView.ChangeColor(node.nodeType == NodeType.Open ? baseColor : wallColor);
                }
            }
        }
    }
}