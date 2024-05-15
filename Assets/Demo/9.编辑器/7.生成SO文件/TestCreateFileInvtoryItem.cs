using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// ���������ļ��Ŀ���ļ�
    /// </summary>
    [CreateAssetMenu(fileName = "TestCreateFileInvtoryItem", menuName = "����SO/�����ļ�/����ļ�")]
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