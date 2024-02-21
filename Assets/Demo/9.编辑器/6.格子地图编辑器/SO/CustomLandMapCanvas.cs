using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Demo.CustomEditors
{
#if UNITY_EDITOR
    /// <summary>
    /// 画布
    /// </summary>
    [CreateAssetMenu(fileName = "CustomLandMapCanvas", menuName = "自定义编辑器/网格地图编辑器")]
    public class CustomLandMapCanvas : ScriptableObject
    {
        //网格尺寸
        public const int gridSize = 70;
        public const int row = 10;
        public const int col = 8;

        public List<CustomLandMapNode> NodeList = new List<CustomLandMapNode>();

        public void Initialize()
        {
            foreach (var node in NodeList)
            {
                node.Initilize();
            }
        }
    }
#endif
}