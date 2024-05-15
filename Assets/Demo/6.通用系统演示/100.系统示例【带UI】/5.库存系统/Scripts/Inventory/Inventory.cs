using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory Instance;

        [Header("Config")]
        [SerializeField] private int inventorySize;
        [SerializeField] private InventoryItem[] inventoryItems;

        [Header("Testing")]
        public InventoryItem testItem;

        public int InventorySize => inventorySize;
        public InventoryItem[] InventoryItems => inventoryItems;

        private void Awake()
        {
            Instance = this;
            inventoryItems = new InventoryItem[inventorySize];
        }

        private void Update()
        {
            //����Slotˢ��
            if (Input.GetKeyDown(KeyCode.H))
            {
                AddItem(testItem.CopyItem(), 3);
            }
        }

        #region �����Ʒ
        /// <summary>
        /// �����Ʒ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        public void AddItem(InventoryItem item, int quantity)
        {
            if (item == null || quantity <= 0) return;

            var itemIndexes = CheckItemStock(item.ID);
            if (item.IsStackable && itemIndexes.Count > 0)
            {
                //�ɶѵ�
                foreach (var index in itemIndexes)
                {
                    int maxStack = item.MaxStack;
                    if (inventoryItems[index].Quantity < maxStack)
                    {
                        inventoryItems[index].Quantity += quantity;
                        //�ж��Ƿ���ʣ��
                        if (inventoryItems[index].Quantity > maxStack)
                        {
                            int dif = inventoryItems[index].Quantity - maxStack;
                            inventoryItems[index].Quantity = maxStack;
                            AddItem(item, dif);//�ݹ����¿�ʼ��ʽ��Ʒ��ֱ���Ų��º����յ�slot
                        }

                        InventoryUI.Instance.DrawItem(inventoryItems[index], index);
                        return;
                    }
                }
            }

            //һ�������һ����Ʒ
            //��Գ�������slot��������ͨ���ݹ�������
            int quantityToAdd = quantity > item.MaxStack ? item.MaxStack : quantity;
            AddItemFreeSlot(item, quantityToAdd);
            int remainingAmount = quantity - quantityToAdd;
            if (remainingAmount > 0)
            {
                AddItem(item, remainingAmount);//�ݹ�������
            }
        }

        /// <summary>
        /// ����Ʒ��ʾ���յ�slot��
        /// </summary>
        /// <param name="item"></param>
        /// <param name="quantity"></param>
        private void AddItemFreeSlot(InventoryItem item, int quantity)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (inventoryItems[i] != null) continue;
                inventoryItems[i] = item.CopyItem();
                inventoryItems[i].Quantity = quantity;
                InventoryUI.Instance.DrawItem(inventoryItems[i], i);
                return;
            }
        }

        /// <summary>
        /// ��ȡ��ͬ��Ʒ�Ŀ��λ����ֵ
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private List<int> CheckItemStock(string itemId)
        {
            List<int> itemIndexes = new List<int>();

            for (int i = 0; i < inventoryItems.Length; i++)
            {
                if (inventoryItems[i] == null) continue;
                if (inventoryItems[i].ID == itemId)
                {
                    itemIndexes.Add(i);
                }
            }
            return itemIndexes;
        }
        #endregion

        #region ������Ʒ

        public void UseItem(int index)
        {
            if (inventoryItems[index] == null) return;
            if (inventoryItems[index].UseItem())
            {
                //ֻ���������Ʒ���п�����
                DecreaseItemStack(index);
            }
        }

        /// <summary>
        /// �Ƴ���Ʒ
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItem(int index)
        {
            if (inventoryItems[index] == null) return;
            inventoryItems[index].RemoveItem();//������Ʒ���Ƴ�����
            inventoryItems[index] = null;
            InventoryUI.Instance.DrawItem(null, index);
        }

        /// <summary>
        /// ���ٿ������
        /// </summary>
        /// <param name="index"></param>
        private void DecreaseItemStack(int index)
        {
            inventoryItems[index].Quantity--;
            if (inventoryItems[index].Quantity <= 0)
            {
                inventoryItems[index] = null;
                InventoryUI.Instance.DrawItem(null, index);//��UI����Ե���Ʒ������Ϊ��
            }
            else
            {
                InventoryUI.Instance.DrawItem(inventoryItems[index], index);
            }

        }
        #endregion

        #region װ��

        /// <summary>
        /// ����װ��
        /// </summary>
        /// <param name="index"></param>
        public void EquipItem(int index)
        {
            if (inventoryItems[index] == null) return;
            if (inventoryItems[index].ItemType != ItemType.Weapon) return;
            inventoryItems[index].EquipItem();
        }

        #endregion
    }
}