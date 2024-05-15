using System;
using UnityEngine;

namespace Demo.Editors.PropertiesEditors.LayoutButton
{
    /// <summary>
    /// 按钮显示
    /// </summary>
    public class LayoutButtonDisplayManager : MonoBehaviour
    {
        [SerializeField] public LayoutButtonDisplayItem[] items;

        /// <summary>
        /// 添加项目
        /// </summary>
        public void Add()
        {
            LayoutButtonDisplayItem newItem = new LayoutButtonDisplayItem();

            if (items?.Length > 0)
            {
                //添加数组时通过拷贝方式添加数据
                var tempArray = new LayoutButtonDisplayItem[items.Length + 1];
                items.CopyTo(tempArray, 0);
                tempArray[tempArray.Length - 1] = newItem;
                items = new LayoutButtonDisplayItem[tempArray.Length];
                tempArray.CopyTo(items, 0);
            }
            else items = new LayoutButtonDisplayItem[] { newItem };
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        public void Delete(int index)
        {
            if (items?.Length == 1) items = Array.Empty<LayoutButtonDisplayItem>();
            else if (items?.Length > 1)
            {
                var tempAudioInfos = new LayoutButtonDisplayItem[items.Length - 1];
                int v_index = 0;
                for (int i = 0; i < items.Length; i++)
                {
                    if (i != index) tempAudioInfos[v_index++] = items[i];
                }
                items = new LayoutButtonDisplayItem[tempAudioInfos.Length];
                tempAudioInfos.CopyTo(items, 0);
            }
        }
    }
}
