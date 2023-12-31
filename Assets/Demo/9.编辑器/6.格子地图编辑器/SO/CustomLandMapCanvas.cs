using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// ����
    /// </summary>
    [CreateAssetMenu(fileName = "CustomLandMapCanvas", menuName = "�Զ���༭��/�����ͼ�༭��")]
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
}