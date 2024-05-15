using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// ��ʾ�򿪱༭������
    /// </summary>
#if UNITY_EDITOR
    public class OpenWindowDemoEditor : EditorWindow
    {
        private static OpenWindowDemoSO _so;

        [MenuItem("�򿪱༭��", menuItem = "�Զ���༭����ʾ/1.�򿪱༭��")]
        private static void OpenWindow()
        {
            //�����´���/��ȡ�Ѵ����Ĵ���
            GetWindow<OpenWindowDemoEditor>("�򿪱༭��");
        }

        [OnOpenAsset(0)]
        public static bool OnDoubleClickAsset(int instanceID, int line)
        {
            //ͨ��˫�������ʵ��id������ת��Ϊ����
            OpenWindowDemoSO so = EditorUtility.InstanceIDToObject(instanceID) as OpenWindowDemoSO;
            if (so == null) return false;

            OpenWindow();
            _so = so;

            return true;
        }
    }
#endif
}