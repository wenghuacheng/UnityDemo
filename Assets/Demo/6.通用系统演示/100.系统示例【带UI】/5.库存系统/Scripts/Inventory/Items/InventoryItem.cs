using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// 物品类型
    /// </summary>
    public enum ItemType
    {
        Weapon,
        Potion,
        Scoll,
        Ingredients,
        Treasure
    }


    /// <summary>
    /// 库存物品
    /// </summary>
    [CreateAssetMenu(fileName = "InventoryItem_", menuName = "带UI演示/库存系统/InventoryItem")]
    public class InventoryItem : ScriptableObject
    {
        [Header("Config")]
        public string ID;
        public string Name;
        public Sprite Icon;
        [TextArea] public string Description;

        [Header("Info")]
        public ItemType ItemType;
        public bool IsConsumable;//是否为消耗品
        public bool IsStackable;//是否可堆叠
        public int MaxStack;//最大堆叠数量

        [HideInInspector] public int Quantity;//当前数量

        public InventoryItem CopyItem()
        {
            InventoryItem instance=Instantiate(this);
            return instance;
        }

        public virtual bool UseItem()
        {
            return false;
        }

        public virtual void EquipItem()
        {

        }

        public virtual void RemoveItem()
        {


        }
    }
}