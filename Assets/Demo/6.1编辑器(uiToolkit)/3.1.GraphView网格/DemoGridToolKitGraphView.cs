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
    /// ��ʾ����
    /// </summary>
    public class DemoGridToolKitGraphView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<DemoGridToolKitGraphView, GraphView.UxmlTraits>
        {

        }


        public DemoGridToolKitGraphView()
        {
            Insert(0, new GridBackground());//��ӱ���������Ҫ��uss�������Ӧ����ʽ��
            
            //���������϶��ƶ��Ȳ���
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            //����������ʽ
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Demo/6.1�༭��(uiToolkit)/3.1.GraphView����/UI/DemoGridToolKitEditor.uss");
            styleSheets.Add(styleSheet);
        }

    }
}