using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    /// <summary>
    /// 装备物品【人物装备中】
    /// </summary>
    public class EquipmentInventoryManager : MonoBehaviour
    {
        public static EquipmentInventoryManager instance;

        [Header("库存物品")]
        public List<InventoryItem> equipmentItems;//库存对象【记录物品数量等信息】
        public Dictionary<ItemData_Equipment, InventoryItem> equipmentInventoryDict;//物品类型与库存对象的映射

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
            equipmentInventoryDict = new Dictionary<ItemData_Equipment, InventoryItem>();

            invertoryItemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>(true);

            UpdateSlotUI();
        }


        /// <summary>
        /// 将物品刷新到物品栏显示
        /// </summary>
        private void UpdateSlotUI()
        {
            for (int i = 0; i < invertoryItemSlots.Length; i++)
            {
                invertoryItemSlots[i].CleanUpSlot();
            }

            for (int i = 0; i < equipmentItems.Count; i++)
            {
                invertoryItemSlots[i].UpdateSlot(equipmentItems[i]);
            }
        }

        /// <summary>
        /// 穿上装备
        /// </summary>
        /// <param name="_item"></param>
        public void EquipItem(ItemData _item)
        {
            ItemData_Equipment newEquipment = _item as ItemData_Equipment;
            InventoryItem newItem = new InventoryItem(newEquipment);

            ItemData_Equipment oldEquipment = null;

            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentInventoryDict)
            {
                //相同类型的装备使用替换的方式
                if (item.Key.equipmentType == newEquipment.equipmentType)
                    oldEquipment = item.Key;
            }

            if (oldEquipment != null)
            {
                UnequipItem(oldEquipment);
                AddItem(oldEquipment);
            }


            equipmentItems.Add(newItem);
            equipmentInventoryDict.Add(newEquipment, newItem);
            newEquipment.AddModifiers();

            RemoveItem(_item);

            UpdateSlotUI();
        }

        /// <summary>
        /// 卸下装备
        /// </summary>
        /// <param name="itemToRemove"></param>
        public void UnequipItem(ItemData_Equipment itemToRemove)
        {
            if (equipmentInventoryDict.TryGetValue(itemToRemove, out InventoryItem value))
            {
                equipmentItems.Remove(value);
                equipmentInventoryDict.Remove(itemToRemove);
                itemToRemove.RemoveModifiers();
            }
        }

        /// <summary>
        /// 将装备添加到物品栏中
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ItemData item)
        {
            InventoryManager.instance.AddItem(item);
        }

        /// <summary>
        /// 将装备从物品栏中移除
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(ItemData item)
        {
            InventoryManager.instance.RemoveItem(item);
        }
    }
}