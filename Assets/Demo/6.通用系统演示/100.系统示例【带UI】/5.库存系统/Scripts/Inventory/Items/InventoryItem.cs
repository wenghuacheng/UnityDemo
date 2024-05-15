using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// ��Ʒ����
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
    /// �����Ʒ
    /// </summary>
    [CreateAssetMenu(fileName = "InventoryItem_", menuName = "��UI��ʾ/���ϵͳ/InventoryItem")]
    public class InventoryItem : ScriptableObject
    {
        [Header("Config")]
        public string ID;
        public string Name;
        public Sprite Icon;
        [TextArea] public string Description;

        [Header("Info")]
        public ItemType ItemType;
        public bool IsConsumable;//�Ƿ�Ϊ����Ʒ
        public bool IsStackable;//�Ƿ�ɶѵ�
        public int MaxStack;//���ѵ�����

        [HideInInspector] public int Quantity;//��ǰ����

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