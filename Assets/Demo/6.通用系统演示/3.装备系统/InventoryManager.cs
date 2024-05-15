using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    /// <summary>
    /// װ�������Ʒ�������С�
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        public static InventoryManager instance;

        public List<InventoryItem> inventoryItems;//�����󡾼�¼��Ʒ��������Ϣ��
        public Dictionary<ItemData, InventoryItem> inventoryDict;//��Ʒ������������ӳ��

        [SerializeField] private Transform inventorySlotParent;//��ƷUI��ĸ�����
        [SerializeField] private List<ItemData> defaultItemData;//Ĭ�ϵ���Ʒ

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
            inventoryItems = new List<InventoryItem>();
            inventoryDict = new Dictionary<ItemData, InventoryItem>();

            invertoryItemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>(true);

            AddDefaultItems();
        }

        /// <summary>
        /// �ṩĬ�ϵı���װ�������ڲ��ԡ�
        /// </summary>
        private void AddDefaultItems()
        {
            for (int i = 0; i < defaultItemData.Count; i++)
            {
                AddItem(defaultItemData[i]);
            }
        }


        /// <summary>
        /// ����Ʒˢ�µ���Ʒ����ʾ
        /// </summary>
        private void UpdateSlotUI()
        {
            //����գ������
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
        /// ��ӿ����Ʒ
        /// </summary>
        public void AddItem(ItemData itemData)
        {
            if (inventoryDict.TryGetValue(itemData, out InventoryItem item))
            {
                //��Ʒ���д�����Ʒ��ֱ����ӿ��
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
        /// �Ƴ������Ʒ
        /// </summary>
        public void RemoveItem(ItemData itemData)
        {
            if (inventoryDict.TryGetValue(itemData, out InventoryItem item))
            {
                if (item.stackSize <= 1)
                {
                    //��Ʒ���Ϊһʱ���ӿ�����Ƴ���Ʒ
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