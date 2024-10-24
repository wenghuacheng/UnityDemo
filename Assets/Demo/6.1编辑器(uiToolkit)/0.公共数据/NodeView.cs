using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Demo.CustomEditors.ToolKit
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public NodeDataSO node;
        public Port input;
        public Port output;

        public NodeView(NodeDataSO node)
        {
            this.node = node;
            this.title = node.name;
            this.viewDataKey = node.guid;

            style.left = node.position.x;
            style.top = node.position.y;        
        }


        /// <summary>
        /// 创建Input连线
        /// </summary>
        public void CreateInputPorts()
        {
            //每次调用会多出一个孔
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

            if (input != null)
            {
                input.portName = "";
                inputContainer.Add(input);
            }
        }

        /// <summary>
        /// 创建Output连线
        /// </summary>
        public void CreateOutputPorts()
        {
            //参数Capacity.Multi代表可以有多个输出连线
            output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            if (output != null)
            {
                output.portName = "";
                outputContainer.Add(output);
            }
        }


        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);

            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin;
        }

    }
}
