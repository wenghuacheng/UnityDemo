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
    /// »­²¼
    /// </summary>
    [CreateAssetMenu(fileName = "CustomLandMapCanvas", menuName = "²âÊÔSO/×Ô¶¨Òå±à¼­Æ÷/Íø¸ñµØÍ¼±à¼­Æ÷")]
    public class CustomLandMapCanvas : ScriptableObject
    {
        //Íø¸ñ³ß´ç
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