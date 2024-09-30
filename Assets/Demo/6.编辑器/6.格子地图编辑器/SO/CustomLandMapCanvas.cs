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
    /// ����
    /// </summary>
    [CreateAssetMenu(fileName = "CustomLandMapCanvas", menuName = "����SO/�Զ���༭��/�����ͼ�༭��")]
    public class CustomLandMapCanvas : ScriptableObject
    {
        //����ߴ�
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