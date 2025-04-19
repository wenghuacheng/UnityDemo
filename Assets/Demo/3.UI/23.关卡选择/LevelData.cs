using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    /// <summary>
    /// �ؿ�����
    /// </summary>
    [CreateAssetMenu(menuName = "UI/�ؿ�ѡ��/�ؿ�����", fileName = "Level Item")]
    public class LevelData : ScriptableObject
    {
        public string LevelID;

        public string LevelName;

        public bool IsLocked ;

        public GameObject LevelButtonObj { get; set; }
    }
}