using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        [Header("Config")]
        [SerializeField] private int inventorySize;
        [SerializeField] private InventoryItem[] inventoryItems;

        [Header("Testing")]
        public InventoryItem testItem;

        public int InventorySize => inventorySize;
        public InventoryItem[] InventoryItems => inventoryItems;

        private void Awake()
        {
            Instance = this;
            inventoryItems = new InventoryItem[inventorySize];
        }

        private void Update()
        {
            //测试Slot刷新
            if (Input.GetKeyDown(KeyCode.H))
            {
                AddItem(testItem.CopyItem(), 3);
            }
        }

        #region 添加物品
        /// <summary>
        /// 添加物品
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public void AddItem(InventoryItem item, int quantity)
        {
            if (item == null || quantity <= 0) return;

            var itemIndexes = CheckItemStock(item.ID);
            if (item.IsStackable && itemIndexes.Count > 0)
            {
                //可堆叠
                foreach (var index in itemIndexes)
                {
                    int maxStack = item.MaxStack;
                    if (inventoryItems[index].Quantity < maxStack)
                    {
                        inventoryItems[index].Quantity += quantity;
                        //判断是否有剩余
                        if (inventoryItems[index].Quantity > maxStack)
                        {
                            int dif = inventoryItems[index].Quantity - maxStack;
                            inventoryItems[index].Quantity = maxStack;
                            AddItem(item, dif);//递归重新开始方式物品，直到放不下后放入空的slot
                        }

                        InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                        return;
                    }
                }
            }

            //一次性添加一组物品
            //针对超过单个slot库存的数量通过递归继续添加
            int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity;
            AddItemFreeSlot(item, quantityToAdd);
            int remainingAmount = quantity - quantityToAdd;
            if (remainingAmount > 0)
            {
                AddItem(item, remainingAmount);//递归继续添加
            }
        }

        /// <summary>
        /// 将物品显示到空的slot上
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        private void AddItemFreeSlot(InventoryItem item, int quantity)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (inventoryItems[i] != null) continue;
                inventoryItems[i] = item.CopyItem();
                inventoryItems[i].Quantity = quantity;
                InventoryUI.Instance.DrawItem(inventoryItems[i], i);
                return;
            }
        }

        /// <summary>
        /// 获取相同物品的库存位索引值
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private List<int> CheckItemStock(string itemId)
        {
            List<int> itemIndexes = new List<int>();

            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i] == null) continue;
                if (inventoryItems[i].ID == itemId)
                {
                    itemIndexes.Add(i);
                }
            }
            return itemIndexes;
        }
        #endregion

        #region 消耗物品

        public void UseItem(int index)
        {
            if (inventoryItems[index] == null) return;
            if (inventoryItems[index].UseItem())
            {
                //只能针对消耗品进行库存减少
                DecreaseItemStack(index);
            }
        }

        /// <summary>
        /// 移除物品
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItem(int index)
        {
            if (inventoryItems[index] == null) return;
            inventoryItems[index].RemoveItem();//调用物品的移除方法
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index);
        }

        /// <summary>
        /// 减少库存数量
        /// </summary>
        /// <param name="index"></param>
        private void DecreaseItemStack(int index)
        {
            inventoryItems[index].Quantity--;
            if (inventoryItems[index].Quantity <= 0)
            {
                inventoryItems[index] = null;
                InventoryUI.Instance.DrawItem(null, index);//将UI中相对的物品栏设置为空
            }
            else
            {
                InventoryUI.Instance.DrawItem(inventoryItems[index], index);
            }

        }
        #endregion

        #region 装备

        /// <summary>
        /// 穿上装备
        /// </summary>
        /// <param name="index"></param>
        public void EquipItem(int index)
        {
            if (inventoryItems[index] == null) return;
            if (inventoryItems[index].ItemType != ItemType.Weapon) return;
            inventoryItems[index].EquipItem();
        }

        #endregion
    }
}