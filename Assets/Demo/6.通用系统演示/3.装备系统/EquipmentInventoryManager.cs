using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    /// <summary>
    /// װ����Ʒ������װ���С�
    /// </summary>
    public class EquipmentInventoryManager : MonoBehaviour
    {
        public static EquipmentInventoryManager instance;

        [Header("�����Ʒ")]
        public List<InventoryItem> equipmentItems;//�����󡾼�¼��Ʒ��������Ϣ��
        public Dictionary<ItemData_Equipment, InventoryItem> equipmentInventoryDict;//��Ʒ������������ӳ��

        [Header("���UI")]
        [SerializeField] private Transform inventorySlotParent;//��ƷUI�ؼ��ű�����

        private UI_ItemSlot[] invertoryItemSlots;//����UI�ϵĿ����ʾ�ű�

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
        /// ����Ʒˢ�µ���Ʒ����ʾ
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
        /// ����װ��
        /// </summary>
        /// <param name="_item"></param>
        public void EquipItem(ItemData _item)
        {
            ItemData_Equipment newEquipment = _item as ItemData_Equipment;
            InventoryItem newItem = new InventoryItem(newEquipment);

            ItemData_Equipment oldEquipment = null;

            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> item in equipmentInventoryDict)
            {
                //��ͬ���͵�װ��ʹ���滻�ķ�ʽ
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
        /// ж��װ��
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
        /// ��װ����ӵ���Ʒ����
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(ItemData item)
        {
            InventoryManager.instance.AddItem(item);
        }

        /// <summary>
        /// ��װ������Ʒ�����Ƴ�
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(ItemData item)
        {
            InventoryManager.instance.RemoveItem(item);
        }
    }
}