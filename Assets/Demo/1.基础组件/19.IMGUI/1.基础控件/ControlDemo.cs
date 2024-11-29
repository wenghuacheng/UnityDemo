using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    public class ControlDemo : MonoBehaviour
    {
        #region �ֶ�
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
            //�����ؼ�
            Demo1();
            //�����
            Demo2();
            //ѡ��
            Demo3();
            //����
            Demo4();
            //����
            Demo5();

        }

        /// <summary>
        /// ��ť�ؼ�
        /// </summary>
        private void Demo1()
        {
            Vector2 startPos = new Vector2(0, 0);

            GUI.Box(new Rect(startPos.x + 10, startPos.y + 10, 200, 110), "��ť��ʾ");

            //Button
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 40, 40, 30), "�ı�:");
            if (GUI.Button(new Rect(startPos.x + 100, startPos.y + 40, 100, 30), "��ť��ʾ"))
            {
                // ��� Button ʱִ�д˴���
                Debug.Log("Button�������");
            }

            //RepeatButton
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 80, 40, 30), "�ı�:");
            if (GUI.RepeatButton(new Rect(startPos.x + 100, startPos.y + 80, 100, 30), "�ظ���ť��ʾ"))
            {
                // ��סʱÿ֡�������
                Debug.Log("RepeatButton�������");
            }
        }


        /// <summary>
        /// �����
        /// </summary>
        private void Demo2()
        {
            Vector2 startPos = new Vector2(0, 120);

            GUI.Box(new Rect(startPos.x + 10, startPos.y + 10, 200, 110), "�������ʾ");

            //TextField
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 40, 70, 30), "���������:");
            textFieldString = GUI.TextField(new Rect(startPos.x + 120, startPos.y + 40, 80, 30), textFieldString);

            //TextArea
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 80, 70, 30), "���������:");
            textAreaString = GUI.TextArea(new Rect(startPos.x + 120, startPos.y + 80, 80, 30), textAreaString);
        }

        /// <summary>
        /// ѡ��
        /// </summary>
        private void Demo3()
        {
            Vector2 startPos = new Vector2(0, 240);

            GUI.Box(new Rect(startPos.x + 10, startPos.y + 10, 300, 250), "ѡ��ؼ���ʾ");

            //Toggle
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 40, 70, 30), "��ѡ��:");
            toggleBool = GUI.Toggle(new Rect(startPos.x + 120, startPos.y + 40, 80, 30), toggleBool, "�Ƿ�ѡ��");

            //Toolbar�����ӡ����İ�ť��������ֻ��ѡ��һ��������Radiobutton
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 80, 70, 30), "��ť��:");
            toolbarInt = GUI.Toolbar(new Rect(startPos.x + 25, startPos.y + 110, 250, 30), toolbarInt, new string[] { "Toolbar1", "Toolbar2", "Toolbar3" });
            if (GUI.changed) Debug.Log("Toolbar:" + toolbarInt); //�ڱ���Żᴥ��

            //SelectionGrid ֻ��ѡ��һ��������Radiobutton,����Ϊ����
            GUI.Label(new Rect(startPos.x + 25, startPos.y + 150, 70, 30), "����ť��:");
            selectionGridInt = GUI.SelectionGrid(new Rect(25, startPos.y + 180, 250, 60), selectionGridInt, new string[] { "Grid 1", "Grid 2", "Grid 3", "Grid 4" }, 2);
        }


        /// <summary>
        /// ����
        /// </summary>
        private void Demo4()
        {
            Vector2 startPos = new Vector2(210, 0);

            GUI.Box(new Rect(startPos.x + 10, startPos.y + 10, 300, 220), "������ʾ");

            GUI.Label(new Rect(startPos.x + 25, startPos.y + 40, 70, 30), "����:");

            //VerticalSlider
            vSliderValue = GUI.VerticalSlider(new Rect(startPos.x + 200, startPos.y + 35, 30, 50), vSliderValue, 10.0f, 0.0f);
            //HorizontalSlider
            hSliderValue = GUI.HorizontalSlider(new Rect(startPos.x + 25, startPos.y + 70, 100, 30), hSliderValue, 0.0f, 10.0f);
            //ScrollView
            scrollViewVector = GUI.BeginScrollView(new Rect(startPos.x + 25, startPos.y + 100, 250, 100), scrollViewVector, new Rect(0, 0, 250, 100));//��ʼ ScrollView      
            innerText = GUI.TextArea(new Rect(0, 0, 250, 100), innerText);  // �� ScrollView �з���һЩ����      
            GUI.EndScrollView();  // ���� ScrollView
        }


        /// <summary>
        /// ����
        /// </summary>
        private Rect windowRect = new Rect(520 + 20, 20, 120, 50);
        private void Demo5()
        {
            windowRect = GUI.Window(0, windowRect, WindowFunction, "����");
        }
        void WindowFunction(int windowID)
        {
            // �ڴ˴����ƴ����ڵ��κοؼ�
            GUI.Label(new Rect(0, 20, 100, 30), "Label");
        }
    }
}
