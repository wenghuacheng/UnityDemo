using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    /// <summary>
    /// 装备库存物品【背包中】
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;

        public List<InventoryItem> inventoryItems;//库存对象【记录物品数量等信息】
        public Dictionary<ItemData, InventoryItem> inventoryDict;//物品类型与库存对象的映射

        [SerializeField] private Transform inventorySlotParent;//物品UI格的父容器
        [SerializeField] private List<ItemData> defaultItemData;//默认的物品

        private UI_ItemSlot[] invertoryItemSlots;//所有UI上的库存显示脚本

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        private void Start()
        {
            inventoryItems = new List<InventoryItem>();
            inventoryDict = new Dictionary<ItemData, InventoryItem>();

            invertoryItemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>(true);

            AddDefaultItems();
        }

        /// <summary>
        /// 提供默认的背包装备【用于测试】
        /// </summary>
        private void AddDefaultItems()
        {
            for (int i = 0; i < defaultItemData.Count; i++)
            {
                AddItem(defaultItemData[i]);
            }
        }


        /// <summary>
        /// 将物品刷新到物品栏显示
        /// </summary>
        private void UpdateSlotUI()
        {
            //先清空，后更新
            for (int i = 0; i < invertoryItemSlots.Length; i++)
            {
                invertoryItemSlots[i].CleanUpSlot();
            }

            for (int i = 0; i < inventoryItems.Count; i++)
            {
                invertoryItemSlots[i].UpdateSlot(inventoryItems[i]);
            }
        }


        /// <summary>
        /// 添加库存物品
        /// </summary>
        public void AddItem(ItemData itemData)
        {
            if (inventoryDict.TryGetValue(itemData, out InventoryItem item))
            {
                //物品栏中存在物品，直接添加库存
                item.AddStack();
            }
            else
            {
                InventoryItem newItem = new InventoryItem(itemData);
                inventoryItems.Add(newItem);
                inventoryDict.Add(itemData, newItem);
            }

            UpdateSlotUI();
        }

        /// <summary>
        /// 移除库存物品
        /// </summary>
        public void RemoveItem(ItemData itemData)
        {
            if (inventoryDict.TryGetValue(itemData, out InventoryItem item))
            {
                if (item.stackSize <= 1)
                {
                    //物品库存为一时，从库存中移除物品
                    inventoryItems.Remove(item);
                    inventoryDict.Remove(itemData);
                }
                else
                {
                    item.RemoveStack();
                }
            }

            UpdateSlotUI();
        }
    }
}