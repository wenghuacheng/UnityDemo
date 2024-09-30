using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// 测试生成文件的库存文件
    /// </summary>
    [CreateAssetMenu(fileName = "TestCreateFileInvtoryItem", menuName = "测试SO/生成文件/库存文件")]
    public class TestCreateFileInvtoryItem : ScriptableObject
    {
        public string id;

        public string Name;

        private void OnValidate()
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }
        }

    }
}