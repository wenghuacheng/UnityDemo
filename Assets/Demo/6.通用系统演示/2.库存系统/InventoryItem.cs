using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    /// <summary>
    /// �����Ʒ
    /// </summary>
    [Serializable]
    public class InventoryItem
    {
        //��Ʒ����
        public ItemData itemData;
        //��Ʒ���
        public int stackSize;

        public InventoryItem(ItemData itemData)
        {
            this.itemData = itemData;
        }

        /// <summary>
        /// ��ӿ��
        /// </summary>
        public void AddStack()
        {
            this.stackSize++;
        }

        /// <summary>
        /// ���ٿ��
        /// </summary>
        public void RemoveStack()
        {
            this.stackSize--;
        }
    }
}