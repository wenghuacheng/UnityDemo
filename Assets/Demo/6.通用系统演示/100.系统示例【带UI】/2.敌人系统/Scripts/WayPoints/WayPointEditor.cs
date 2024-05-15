using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 路径点编辑器
    /// </summary>
#if UNITY_EDITOR
    [CustomEditor(typeof(WayPoint))]
    public class WayPointEditor : Editor
    {
        private WayPoint WayPointTarget => target as WayPoint;

        private void OnSceneGUI()
        {
            if (WayPointTarget.Points.Length <= 0) return;

            //绘制出所有的路径点，用于后面拖拽点的位置
            Handles.color = Color.red;
            for (int i = 0; i < WayPointTarget.Points.Length; i++)
            {
                EditorGUI.BeginChangeCheck();

                //当前路径点的世界坐标
                Vector3 currentPoint = WayPointTarget.EntityPosition + WayPointTarget.Points[i];

                //生成一个移动句柄并返回拖动后新的位置
                Vector3 newPosition = Handles.FreeMoveHandle(currentPoint, Quaternion.identity, 0.5f, Vector3.one * 0.5f, Handles.SphereHandleCap);

                //显示当前的路径点索引
                GUIStyle text = new GUIStyle();
                text.fontStyle = FontStyle.Bold;
                text.fontSize = 16;
                text.normal.textColor = Color.black;
                Vector3 textPos = new Vector3(0.2f, -0.2f);//文字位置
                Handles.Label(WayPointTarget.EntityPosition + WayPointTarget.Points[i] + textPos, $"{i + 1}", text);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Free Move");
                    //更新路径点的相对位置
                    WayPointTarget.Points[i] = newPosition - WayPointTarget.EntityPosition;
                }
            }
        }
    }
#endif
}