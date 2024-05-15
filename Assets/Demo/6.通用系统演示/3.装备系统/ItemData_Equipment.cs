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
    /// װ������
    /// </summary>
    [CreateAssetMenu(fileName = "Equipment", menuName = "��UI��ʾ/���ϵͳ/Equipment")]
    public class ItemData_Equipment : ItemData
    {
        public EquipmentType equipmentType;


        /// <summary>
        /// �������Ч��
        /// </summary>
        public void AddModifiers()
        {

        }

        /// <summary>
        /// �Ƴ�����Ч��
        /// </summary>
        public void RemoveModifiers()
        {

        }
    }
}
