using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ·����༭��
    /// </summary>
#if UNITY_EDITOR
    [CustomEditor(typeof(WayPoint))]
    public class WayPointEditor : Editor
    {
        private WayPoint WayPointTarget => target as WayPoint;

        private void OnSceneGUI()
        {
            if (WayPointTarget.Points.Length <= 0) return;

            //���Ƴ����е�·���㣬���ں�����ק���λ��
            Handles.color = Color.red;
            for (int i = 0; i < WayPointTarget.Points.Length; i++)
            {
                EditorGUI.BeginChangeCheck();

                //��ǰ·�������������
                Vector3 currentPoint = WayPointTarget.EntityPosition + WayPointTarget.Points[i];

                //����һ���ƶ�����������϶����µ�λ��
                Vector3 newPosition = Handles.FreeMoveHandle(currentPoint, Quaternion.identity, 0.5f, Vector3.one * 0.5f, Handles.SphereHandleCap);

                //��ʾ��ǰ��·��������
                GUIStyle text = new GUIStyle();
                text.fontStyle = FontStyle.Bold;
                text.fontSize = 16;
                text.normal.textColor = Color.black;
                Vector3 textPos = new Vector3(0.2f, -0.2f);//����λ��
                Handles.Label(WayPointTarget.EntityPosition + WayPointTarget.Points[i] + textPos, $"{i + 1}", text);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Free Move");
                    //����·��������λ��
                    WayPointTarget.Points[i] = newPosition - WayPointTarget.EntityPosition;
                }
            }
        }
    }
#endif
}