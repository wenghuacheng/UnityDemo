using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.CustomEditors.ToolKit
{
    /// <summary>
    /// 演示右键菜单
    /// </summary>
    public class DemoContextMenuToolKitGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<DemoContextMenuToolKitGraphView, GraphView.UxmlTraits>
        {

        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction($"AAA", (a) => Debug.Log(a.name));
            evt.menu.AppendAction($"BBB", (a) => Debug.Log(a.name));
            evt.menu.AppendAction($"CCC", (a) => Debug.Log(a.name));
        }
    }
}
