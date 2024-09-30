using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Demo.CustomEditors
{
    [CustomEditor(typeof(TestCreateFileInvtoryItem))]
    public class TestCreateFileEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TestCreateFileInvtoryItem item = (TestCreateFileInvtoryItem)target;

            if (GUILayout.Button("Generate"))
            {
                // 在这里生成商店物品对象并指定位置
                CreateShopItem(item);
            }
        }

        private void CreateShopItem(TestCreateFileInvtoryItem item)
        {
            // 创建商店物品对象的代码
            // 例如，可以实例化一个新的商店物品对象，并设置其属性
            TestCreateFileShopItem shopItem = ScriptableObject.CreateInstance<TestCreateFileShopItem>();
            shopItem.Name = item.Name + " (Shop)";
            shopItem.item = item;


            //// 将商店物品对象添加到商店的ScriptableObject中
            //ShopInventory shopInventory = AssetDatabase.LoadAssetAtPath<ShopInventory>("Assets/Path/To/Your/ShopInventory.asset");
            //if (shopInventory != null)
            //{
            //    shopInventory.AddItem(shopItem);
            //    EditorUtility.SetDirty(shopInventory); // 标记ScriptableObject已更改
            //    AssetDatabase.SaveAssets(); // 保存更改
            //    AssetDatabase.Refresh(); // 刷新资源
            //}

            string dir = "Assets/Demo/9.编辑器/7.生成SO文件";
            string fileName = Path.Combine(dir, shopItem.Name + ".asset");
            if (File.Exists(fileName))
            {
                return;
            }
            AssetDatabase.CreateAsset(shopItem, fileName);
            AssetDatabase.SaveAssets(); // 保存更改
            AssetDatabase.Refresh(); // 刷新资源
        }

    }
}
