using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Inventory
{
    /// <summary>
    /// ���������ϡ�
    /// </summary>
    public class StashInventoryManager : MonoBehaviour
    {
        public static StashInventoryManager instance;

        [Header("�����Ʒ")]
        public List<InventoryItem> stashInventoryItems;//�����󡾼�¼��Ʒ��������Ϣ��
        public Dictionary<ItemData, InventoryItem> stashInventoryDict;//��Ʒ������������ӳ��

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
            stashInventoryDict = new Dictionary<ItemData, InventoryItem>();

            invertoryItemSlots = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>(true);

            UpdateSlotUI();
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

            for (int i = 0; i < stashInventoryItems.Count; i++)
            {
                invertoryItemSlots[i].UpdateSlot(stashInventoryItems[i]);
            }
        }


        /// <summary>
        /// ��ӿ����Ʒ
        /// </summary>
        public void AddItem(ItemData itemData)
        {
            if (stashInventoryDict.TryGetValue(itemData, out InventoryItem item))
            {
                //��Ʒ���д�����Ʒ��ֱ����ӿ��
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
        /// �Ƴ������Ʒ
        /// </summary>
        public void RemoveItem(ItemData itemData)
        {
            if (stashInventoryDict.TryGetValue(itemData, out InventoryItem item))
            {
                if (item.stackSize <= 1)
                {
                    //��Ʒ���Ϊһʱ���ӿ�����Ƴ���Ʒ
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