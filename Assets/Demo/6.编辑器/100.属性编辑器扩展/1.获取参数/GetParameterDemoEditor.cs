using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.Editors.PropertiesEditors.GetParameter
{
    /// <summary>
    /// 获取参数
    /// </summary>
#if UNITY_EDITOR
    [CustomEditor(typeof(TestManager))]
    public class GetParameterDemoEditor : Editor
    {
        private TestManager _manager;
        private SerializedProperty _testInfos;

        private void OnEnable()
        {
            if (_manager == null) _manager = target as TestManager;//从target中获取对象数据
            if (_testInfos == null) _testInfos = serializedObject.FindProperty("testInfos");//获取序列化后的对象，通过属性获取值
        }

        public override void OnInspectorGUI()
        {
            //通过序列化对象获取值
            for (int i = 0; i < _testInfos?.arraySize; i++)
            {
                SerializedProperty element = _testInfos.GetArrayElementAtIndex(i);
                string name = element.FindPropertyRelative("Name").stringValue;//获取TestInfo中的Name属性
                int count = element.FindPropertyRelative("Count").intValue;//获取TestInfo中的Count属性
                Debug.Log($"Name：{name},Count:{count}");
            }
        }
    }
#endif
}
