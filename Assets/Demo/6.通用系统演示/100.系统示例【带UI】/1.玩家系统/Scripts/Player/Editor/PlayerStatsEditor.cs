using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// ���PlayerStats���͵ı༭����ӹ���
    /// </summary>
    [CustomEditor(typeof(PlayerStats))]
    public class PlayerStatsEditor : Editor
    {
        private PlayerStats StatsTarget => target as PlayerStats;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //��PlayerStats���͵ı༭������Ӹ�λ��ť
            if (GUILayout.Button("Reset Player"))
            {
                StatsTarget.ResetPlayer();
            }
        }
    }
}