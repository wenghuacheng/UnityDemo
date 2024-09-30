using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.Editors.PropertiesEditors.LayoutButton
{
#if UNITY_EDITOR
    [CustomEditor(typeof(LayoutButtonDisplayManager))]
    public class LayoutButtonEditor : Editor
    {
        private LayoutButtonDisplayManager _manager;
        private SerializedProperty items;
        private bool foldout;//չ��״̬���ļ���Ч����

        private void OnEnable()
        {
            if (_manager == null) _manager = (LayoutButtonDisplayManager)target;
            if (items == null) items = serializedObject.FindProperty("items");
        }

        public override void OnInspectorGUI()
        {
            //����ע�͵��Ͳ�����ʾĬ�Ͽؼ���
            //base.OnInspectorGUI();

            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, "items");//���Ƴ���items֮�����������������

            #region ���ɱ�������Ӱ�ť
            EditorGUILayout.BeginHorizontal();
            //����б���ļ��з�ʽ��ʾ
            GUIStyle foldoutStyle = EditorStyles.foldout;
            foldout = EditorGUILayout.Foldout(foldout, $"Item Count:{_manager.items.Length}", foldoutStyle);
            if (GUILayout.Button("Add", GUILayout.MinWidth(40), GUILayout.MaxWidth(50)))
            {
                _manager.Add();
                foldout = true;
            }
            EditorGUILayout.EndHorizontal();
            #endregion

            #region ��ʾ�б�
            if (foldout)
            {
                EditorGUI.indentLevel++;//�����б������һ��

                for (int i = 0; i < items?.arraySize; i++)
                {
                    var item = items.GetArrayElementAtIndex(i);

                    var nameString = item.FindPropertyRelative("Name").stringValue;
                    GUIContent nameContent = new GUIContent(string.IsNullOrWhiteSpace(nameString) ? "element" : nameString);

                    EditorGUILayout.BeginHorizontal();
                    //�����б���������Բ����ñ���
                    EditorGUILayout.PropertyField(item, nameContent, true);
                    if (GUILayout.Button("Del", GUILayout.MinWidth(40), GUILayout.MaxWidth(50)))
                    {
                        _manager.Delete(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUI.indentLevel--;//�ָ�����
            }
            #endregion

            //��д�޸ģ�����ӻᵼ���޸Ĳ���Ч
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}