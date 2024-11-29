using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    public class LayoutControlDemo : MonoBehaviour
    {
        public GUIStyle style;


        /// <summary>
        /// GUI:�̶�����
        /// GUILayout:�Զ�����,����Ҫ����Rect
        /// </summary>
        void OnGUI()
        {
            // //һ���Զ����ֵ�ʾ��
            // GUILayout.Button("I am an Automatic Layout Button");

            //group
            Demo1();
            //Area
            Demo2();
            //ˮƽ����
            Demo3();
            //��ֱ����
            Demo4();
            //�Զ������
            Demo5();
        }


        /// <summary>
        /// ���Խ��̶����ֿؼ�����һ������ĸ��ڵ������Զ�λ
        /// ���ڿ���Ƕ����
        /// </summary>
        private void Demo1()
        {
            //����һ����
            GUI.BeginGroup(new Rect(10, 10, 100, 100));
            //�����ڵĿؼ�����group�����Ͻ���Ϊ��׼��
            {
                GUI.Box(new Rect(0, 0, 100, 100), "Group is here");
                GUI.Button(new Rect(10, 40, 80, 30), "Click me");
            }
            // �����������濪ʼ����
            GUI.EndGroup();
        }


        /// <summary>
        /// Area
        /// ֧���Զ����ֿؼ�
        /// </summary>
        private void Demo2()
        {
            Vector2 startPos = new Vector2(10, 130);

            GUILayout.BeginArea(new Rect(startPos.x, startPos.y, 100, 30));
            GUILayout.Button("I am completely inside an Area");
            GUILayout.EndArea();
        }

        /// <summary>
        /// ˮƽ����
        /// </summary>
        private void Demo3()
        {
            Vector2 startPos = new Vector2(10, 170);

            // �����пؼ�������ָ���� GUI ������
            GUILayout.BeginArea(new Rect(startPos.x, startPos.y, 200, 60));

            GUILayout.Box("ˮƽ����");

            // ��ʼ����ˮƽ��
            GUILayout.BeginHorizontal();

            //���Զ�����ؼ����
            GUILayout.Button("1111");
            GUILayout.Button("2222");
            GUILayout.Button("3333");
            GUILayout.Button("4444");

            GUILayout.EndHorizontal();

            GUILayout.EndArea();
        }

        /// <summary>
        /// ��ֱ����
        /// </summary>
        private void Demo4()
        {
            Vector2 startPos = new Vector2(10, 230);

            // �����пؼ�������ָ���� GUI ������
            GUILayout.BeginArea(new Rect(startPos.x, startPos.y, 60, 200));

            GUILayout.Box("��ֱ����");

            // ��ʼ����ˮƽ��
            GUILayout.BeginVertical();

            //���Զ�����ؼ����
            GUILayout.Button("1111");
            GUILayout.Button("2222");
            GUILayout.Button("3333");
            GUILayout.Button("4444");

            GUILayout.EndVertical();

            GUILayout.EndArea();
        }


        /// <summary>
        /// ��������
        /// </summary>
        private void Demo5()
        {
            Vector2 startPos = new Vector2(130, 10);

            GUILayout.BeginArea(new Rect(startPos.x, startPos.y, 60, 200));
            GUILayout.Button("���95", GUILayout.Width(95));//���ÿ��
            GUILayout.Button("���45", GUILayout.Width(45));//���ÿ��

            GUILayout.EndArea();
        }
    }
}