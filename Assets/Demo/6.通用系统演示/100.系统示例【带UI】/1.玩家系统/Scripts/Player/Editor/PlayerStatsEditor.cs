using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// 针对PlayerStats类型的编辑器添加功能
    /// </summary>
    [CustomEditor(typeof(PlayerStats))]
    public class PlayerStatsEditor : Editor
    {
        private PlayerStats StatsTarget => target as PlayerStats;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            //在PlayerStats类型的编辑器上添加复位按钮
            if (GUILayout.Button("Reset Player"))
            {
                StatsTarget.ResetPlayer();
            }
        }
    }
}