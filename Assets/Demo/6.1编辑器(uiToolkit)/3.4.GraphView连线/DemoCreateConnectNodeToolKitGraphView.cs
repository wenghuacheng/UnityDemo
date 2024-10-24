using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.CustomEditors.ToolKit
{
    public class DemoCreateConnectNodeToolKitGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<DemoCreateConnectNodeToolKitGraphView, GraphView.UxmlTraits>
        {

        }

        public DemoCreateConnectNodeToolKitGraphView()
        {
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction($"创建节点", (a) =>
            {
                var node1 = CreateNode();
                node1.SetPosition(new Rect(10, 10, 30, 30));
                var node2 = CreateNode();
                node2.SetPosition(new Rect(30, 10, 30, 30));
                var node3 = CreateNode();
                node3.SetPosition(new Rect(50, 10, 30, 30));


                //代码进行连接
                var edge = node1.output.ConnectTo(node2.input);
                AddElement(edge);
            });
        }

        private NodeView CreateNode()
        {
            NodeDataSO data = new NodeDataSO();
            data.name = "Node" + Random.Range(1, 10);

            //NodeView使用了系统的节点
            NodeView nodeView = new NodeView(data);

            AddElement(nodeView);
            nodeView.CreateInputPorts();
            nodeView.CreateOutputPorts();

            return nodeView;
        }
    }
}
