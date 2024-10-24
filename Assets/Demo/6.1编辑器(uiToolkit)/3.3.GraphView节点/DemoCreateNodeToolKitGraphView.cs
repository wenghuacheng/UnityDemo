using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.CustomEditors.ToolKit
{
    public class DemoCreateNodeToolKitGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<DemoCreateNodeToolKitGraphView, GraphView.UxmlTraits>
        {

        }

        public DemoCreateNodeToolKitGraphView()
        {
            Insert(0, new GridBackground());//添加背景网格（需要在uss中添加相应的样式）
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction($"创建节点", (a) =>
            {
                NodeDataSO data = new NodeDataSO();
                data.name = "Node" + Random.Range(1, 10);

                //NodeView使用了系统的节点
                NodeView nodeView = new NodeView(data);
                AddElement(nodeView);
            });
        }
    }
}
