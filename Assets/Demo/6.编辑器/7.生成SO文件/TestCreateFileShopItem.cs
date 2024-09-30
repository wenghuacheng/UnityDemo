using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.CustomEditors
{
    /// <summary>
    /// 测试商店文件
    /// </summary>
    [CreateAssetMenu(fileName = "TestCreateFileShopItem", menuName = "测试SO/生成文件/商店文件")]
    public class TestCreateFileShopItem : ScriptableObject
    {
        public TestCreateFileInvtoryItem item;

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
