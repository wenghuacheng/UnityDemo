using Demo.Common.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    public enum EquipmentType
    {
        Weapon,
        Armor
    }

    /// <summary>
    /// 装备数据
    /// </summary>
    [CreateAssetMenu(fileName = "Equipment", menuName = "Inventory/Equipment")]
    public class ItemData_Equipment : ItemData
    {
        public EquipmentType equipmentType;


        /// <summary>
        /// 添加增益效果
        /// </summary>
        public void AddModifiers()
        {

        }

        /// <summary>
        /// 移除增益效果
        /// </summary>
        public void RemoveModifiers()
        {

        }
    }
}
