using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.CustomEditors.ToolKit
{
    /// <summary>
    /// 显示网格
    /// </summary>
    public class DemoGridToolKitGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<DemoGridToolKitGraphView, GraphView.UxmlTraits>
        {

        }


        public DemoGridToolKitGraphView()
        {
            Insert(0, new GridBackground());//添加背景网格（需要在uss中添加相应的样式）
            
            //添加网格的拖动移动等操作
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            //加载网格样式
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Demo/6.1编辑器(uiToolkit)/3.1.GraphView网格/UI/DemoGridToolKitEditor.uss");
            styleSheets.Add(styleSheet);
        }

    }
}