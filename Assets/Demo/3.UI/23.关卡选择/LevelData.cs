using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    /// <summary>
    /// 关卡数据
    /// </summary>
    [CreateAssetMenu(menuName = "UI/关卡选择/关卡数据", fileName = "Level Item")]
    public class LevelData : ScriptableObject
    {
        public string LevelID;

        public string LevelName;

        public bool IsLocked ;

        public GameObject LevelButtonObj { get; set; }
    }
}