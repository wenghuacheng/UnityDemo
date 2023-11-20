using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Demo.CustomEditor
{
    /// <summary>
    /// 演示打开编辑器窗体
    /// </summary>
    public class OpenWindowDemoEditor : EditorWindow
    {
        private static OpenWindowDemoSO _so;

        [MenuItem("打开编辑器", menuItem = "自定义编辑器演示/1.打开编辑器")]
        private static void OpenWindow()
        {
            //创建新窗体/获取已创建的窗体
            GetWindow<OpenWindowDemoEditor>("打开编辑器");
        }

        
        [OnOpenAsset(0)]
        public static bool OnDoubleClickAsset(int instanceID, int line)
        {
            //通过双击对象的实例id，将其转化为对象
            OpenWindowDemoSO so = EditorUtility.InstanceIDToObject(instanceID) as OpenWindowDemoSO;
            if (so == null) return false;

            OpenWindow();
            _so = so;

            return true;
        }
    }
}