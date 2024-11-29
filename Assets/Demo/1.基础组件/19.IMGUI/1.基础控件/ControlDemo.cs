using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    public class ControlDemo : MonoBehaviour
    {
        #region 字段
        private string textFieldString = "text field";
        private string textAreaString = "text area";
        private bool toggleBool = true;
        private int toolbarInt = 0;
        private int selectionGridInt = 0;
        private float vSliderValue = 0.0f;
        private float hSliderValue = 0.0f;
        private Vector2 scrollViewVector = Vector2.zero;
        private string innerText = "I am inside the ScrollView111111111111111111111111111111111111112222222222222222222222222222222222222222222222223333333333333333333333333333333333333555555555555555555555";

        #endregion

        void OnGUI()
        {
            //基础控件
            Demo1();
            //输入框
            Demo2();
            //选择
            Demo3();
            //滑块
            Demo4();
            //窗体
            Demo5();

        }

        /// <summary>
        /// 按钮控件
        /// </summary>
        private void Demo1()
        {
            Vector2 startPos = new Vector2(0, 0);

            GUI.Box(new Rect(startPos.x + 10, startPos.y + 10, 200, 110), "按钮演示");

            //Button
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 40, 40, 30), "文本:");
            if (GUI.Button(new Rect(startPos.x + 100, startPos.y + 40, 100, 30), "按钮演示"))
            {
                // 点击 Button 时执行此代码
                Debug.Log("Button被点击了");
            }

            //RepeatButton
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 80, 40, 30), "文本:");
            if (GUI.RepeatButton(new Rect(startPos.x + 100, startPos.y + 80, 100, 30), "重复按钮演示"))
            {
                // 按住时每帧都会输出
                Debug.Log("RepeatButton被点击了");
            }
        }


        /// <summary>
        /// 输入框
        /// </summary>
        private void Demo2()
        {
            Vector2 startPos = new Vector2(0, 120);

            GUI.Box(new Rect(startPos.x + 10, startPos.y + 10, 200, 110), "输入框演示");

            //TextField
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 40, 70, 30), "单行输入框:");
            textFieldString = GUI.TextField(new Rect(startPos.x + 120, startPos.y + 40, 80, 30), textFieldString);

            //TextArea
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 80, 70, 30), "多行输入框:");
            textAreaString = GUI.TextArea(new Rect(startPos.x + 120, startPos.y + 80, 80, 30), textAreaString);
        }

        /// <summary>
        /// 选择
        /// </summary>
        private void Demo3()
        {
            Vector2 startPos = new Vector2(0, 240);

            GUI.Box(new Rect(startPos.x + 10, startPos.y + 10, 300, 250), "选择控件演示");

            //Toggle
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 40, 70, 30), "勾选框:");
            toggleBool = GUI.Toggle(new Rect(startPos.x + 120, startPos.y + 40, 80, 30), toggleBool, "是否选择");

            //Toolbar（会打印点击的按钮索引），只能选择一个类似于Radiobutton
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 80, 70, 30), "按钮组:");
            toolbarInt = GUI.Toolbar(new Rect(startPos.x + 25, startPos.y + 110, 250, 30), toolbarInt, new string[] { "Toolbar1", "Toolbar2", "Toolbar3" });
            if (GUI.changed) Debug.Log("Toolbar:" + toolbarInt); //在变更才会触发

            //SelectionGrid 只能选择一个类似于Radiobutton,布局为网格
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 150, 70, 30), "网格按钮组:");
            selectionGridInt = GUI.SelectionGrid(new Rect(25, startPos.y + 180, 250, 60), selectionGridInt, new string[] { "Grid 1", "Grid 2", "Grid 3", "Grid 4" }, 2);
        }


        /// <summary>
        /// 滑块
        /// </summary>
        private void Demo4()
        {
            Vector2 startPos = new Vector2(210, 0);

            GUI.Box(new Rect(startPos.x + 10, startPos.y + 10, 300, 220), "滑块演示");

            GUI.Label(new Rect(startPos.x + 25, startPos.y + 40, 70, 30), "滑块:");

            //VerticalSlider
            vSliderValue = GUI.VerticalSlider(new Rect(startPos.x + 200, startPos.y + 35, 30, 50), vSliderValue, 10.0f, 0.0f);
            //HorizontalSlider
            hSliderValue = GUI.HorizontalSlider(new Rect(startPos.x + 25, startPos.y + 70, 100, 30), hSliderValue, 0.0f, 10.0f);
            //ScrollView
            scrollViewVector = GUI.BeginScrollView(new Rect(startPos.x + 25, startPos.y + 100, 250, 100), scrollViewVector, new Rect(0, 0, 250, 100));//开始 ScrollView      
            innerText = GUI.TextArea(new Rect(0, 0, 250, 100), innerText);  // 在 ScrollView 中放入一些内容      
            GUI.EndScrollView();  // 结束 ScrollView
        }


        /// <summary>
        /// 窗体
        /// </summary>
        private Rect windowRect = new Rect(520 + 20, 20, 120, 50);
        private void Demo5()
        {
            windowRect = GUI.Window(0, windowRect, WindowFunction, "窗体");
        }
        void WindowFunction(int windowID)
        {
            // 在此处绘制窗口内的任何控件
            GUI.Label(new Rect(0, 20, 100, 30), "Label");
        }
    }
}
