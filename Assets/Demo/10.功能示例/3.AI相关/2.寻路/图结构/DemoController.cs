using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.AI.PathFind.GraphWay
{
    public class DemoController : MonoBehaviour
    {
        public MapData mapData;
        public Graph graph;

        private void Start()
        {
            if (mapData != null && graph != null)
            {
                int[,] map = mapData.InitializeMap();
                graph.Initialize(map);

                var graphView = graph.gameObject.GetComponent<GraphView>();
                graphView.Init(graph);
            }
        }
    }
}