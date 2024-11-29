using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    public class LayoutControlDemo : MonoBehaviour
    {
        public GUIStyle style;


        /// <summary>
        /// GUI:固定布局
        /// GUILayout:自动布局,不需要设置Rect
        /// </summary>
        void OnGUI()
        {
            // //一个自动布局的示例
            // GUILayout.Button("I am an Automatic Layout Button");

            //group
            Demo1();
            //Area
            Demo2();
            //水平布局
            Demo3();
            //垂直布局
            Demo4();
            //自定义参数
            Demo5();
        }


        /// <summary>
        /// 可以将固定布局控件基于一个虚拟的父节点进行相对定位
        /// 组内可以嵌套组
        /// </summary>
        private void Demo1()
        {
            //创建一个组
            GUI.BeginGroup(new Rect(10, 10, 100, 100));
            //区域内的控件都是group的左上角作为基准点
            {
                GUI.Box(new Rect(0, 0, 100, 100), "Group is here");
                GUI.Button(new Rect(10, 40, 80, 30), "Click me");
            }
            // 结束我们上面开始的组
            GUI.EndGroup();
        }


        /// <summary>
        /// Area
        /// 支持自动布局控件
        /// </summary>
        private void Demo2()
        {
            Vector2 startPos = new Vector2(10, 130);

            GUILayout.BeginArea(new Rect(startPos.x, startPos.y, 100, 30));
            GUILayout.Button("I am completely inside an Area");
            GUILayout.EndArea();
        }

        /// <summary>
        /// 水平布局
        /// </summary>
        private void Demo3()
        {
            Vector2 startPos = new Vector2(10, 170);

            // 将所有控件包裹在指定的 GUI 区域中
            GUILayout.BeginArea(new Rect(startPos.x, startPos.y, 200, 60));

            GUILayout.Box("水平布局");

            // 开始单个水平组
            GUILayout.BeginHorizontal();

            //会自动分配控件宽度
            GUILayout.Button("1111");
            GUILayout.Button("2222");
            GUILayout.Button("3333");
            GUILayout.Button("4444");

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        /// <summary>
        /// 垂直布局
        /// </summary>
        private void Demo4()
        {
            Vector2 startPos = new Vector2(10, 230);

            // 将所有控件包裹在指定的 GUI 区域中
            GUILayout.BeginArea(new Rect(startPos.x, startPos.y, 60, 200));

            GUILayout.Box("垂直布局");

            // 开始单个水平组
            GUILayout.BeginVertical();

            //会自动分配控件宽度
            GUILayout.Button("1111");
            GUILayout.Button("2222");
            GUILayout.Button("3333");
            GUILayout.Button("4444");

            GUILayout.EndVertical();

            GUILayout.EndArea();
        }


        /// <summary>
        /// 参数设置
        /// </summary>
        private void Demo5()
        {
            Vector2 startPos = new Vector2(130, 10);

            GUILayout.BeginArea(new Rect(startPos.x, startPos.y, 60, 200));
            GUILayout.Button("宽度95", GUILayout.Width(95));//设置宽度
            GUILayout.Button("宽度45", GUILayout.Width(45));//设置宽度

            GUILayout.EndArea();
        }
    }
}