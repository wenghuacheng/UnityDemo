using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    /// <summary>
    /// 库存管理【材料】
    /// </summary>
    public class StashInventoryManager : MonoBehaviour
    {
        public static StashInventoryManager instance;

        [Header("库存物品")]
        public List<InventoryItem> stashInventoryItems;//库存对象【记录物品数量等信息】
        public Dictionary<ItemData, InventoryItem> stashInventoryDict;//物品类型与库存对象的映射

        [Header("库存UI")]
        [SerializeField] private Transform inventorySlotParent;//物品UI控件脚本集合

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
            //inventoryItems = new List<InventoryItem>();
            stashInventoryDict = new Dictionary<ItemData, InventoryItem>();

            invertoryItemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>(true);

            UpdateSlotUI();
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

            for (int i = 0; i < stashInventoryItems.Count; i++)
            {
                invertoryItemSlots[i].UpdateSlot(stashInventoryItems[i]);
            }
        }


        /// <summary>
        /// 添加库存物品
        /// </summary>
        public void AddItem(ItemData itemData)
        {
            if (stashInventoryDict.TryGetValue(itemData, out InventoryItem item))
            {
                //物品栏中存在物品，直接添加库存
                item.AddStack();
            }
            else
            {
                InventoryItem newItem = new InventoryItem(itemData);
                stashInventoryItems.Add(newItem);
                stashInventoryDict.Add(itemData, newItem);
            }

            UpdateSlotUI();
        }

        /// <summary>
        /// 移除库存物品
        /// </summary>
        public void RemoveItem(ItemData itemData)
        {
            if (stashInventoryDict.TryGetValue(itemData, out InventoryItem item))
            {
                if (item.stackSize <= 1)
                {
                    //物品库存为一时，从库存中移除物品
                    stashInventoryItems.Remove(item);
                    stashInventoryDict.Remove(itemData);
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