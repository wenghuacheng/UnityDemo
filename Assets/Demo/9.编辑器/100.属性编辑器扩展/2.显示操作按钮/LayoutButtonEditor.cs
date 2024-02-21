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
        private bool foldout;//展开状态【文件夹效果】

        private void OnEnable()
        {
            if (_manager == null) _manager = (LayoutButtonDisplayManager)target;
            if (items == null) items = serializedObject.FindProperty("items");
        }

        public override void OnInspectorGUI()
        {
            //这里注释掉就不会显示默认控件的
            //base.OnInspectorGUI();

            serializedObject.Update();
            DrawPropertiesExcluding(serializedObject, "items");//绘制除了items之外的所有其他的属性

            #region 生成标题与添加按钮
            EditorGUILayout.BeginHorizontal();
            //针对列表的文件夹方式显示
            GUIStyle foldoutStyle = EditorStyles.foldout;
            foldout = EditorGUILayout.Foldout(foldout, $"Item Count:{_manager.items.Length}", foldoutStyle);
            if (GUILayout.Button("Add", GUILayout.MinWidth(40), GUILayout.MaxWidth(50)))
            {
                _manager.Add();
                foldout = true;
            }
            EditorGUILayout.EndHorizontal();
            #endregion

            #region 显示列表
            if (foldout)
            {
                EditorGUI.indentLevel++;//所有列表项都缩进一级

                for (int i = 0; i < items?.arraySize; i++)
                {
                    var item = items.GetArrayElementAtIndex(i);

                    var nameString = item.FindPropertyRelative("Name").stringValue;
                    GUIContent nameContent = new GUIContent(string.IsNullOrWhiteSpace(nameString) ? "element" : nameString);

                    EditorGUILayout.BeginHorizontal();
                    //绘制列表的所有属性并设置标题
                    EditorGUILayout.PropertyField(item, nameContent, true);
                    if (GUILayout.Button("Del", GUILayout.MinWidth(40), GUILayout.MaxWidth(50)))
                    {
                        _manager.Delete(i);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUI.indentLevel--;//恢复缩进
            }
            #endregion

            //回写修改，不添加会导致修改不生效
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}