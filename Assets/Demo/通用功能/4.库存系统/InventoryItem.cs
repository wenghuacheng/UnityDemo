using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    /// <summary>
    /// 库存物品
    /// </summary>
    [Serializable]
    public class InventoryItem
    {
        //物品数据
        public ItemData itemData;
        //物品库存
        public int stackSize;

        public InventoryItem(ItemData itemData)
        {
            this.itemData = itemData;
        }

        /// <summary>
        /// 添加库存
        /// </summary>
        public void AddStack()
        {
            this.stackSize++;
        }

        /// <summary>
        /// 减少库存
        /// </summary>
        public void RemoveStack()
        {
            this.stackSize--;
        }
    }
}