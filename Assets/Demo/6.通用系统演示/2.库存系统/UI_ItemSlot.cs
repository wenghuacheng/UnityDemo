using Demo.Common.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Demo.Common.Inventory
{
    /// <summary>
    /// �����ʾ��������Ʒ��UI�ű���
    /// </summary>
    public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemText;

        public InventoryItem item;

        /// <summary>
        /// ˢ��UI
        /// </summary>
        /// <param name="newItem"></param>
        public void UpdateSlot(InventoryItem newItem)
        {
            this.item = newItem;

            if (item != null)
            {
                itemImage.color = Color.white;//Ĭ������Ϊ��͸��

                itemImage.sprite = item.itemData.icon;
                if (item.stackSize >= 1)
                {
                    itemText.text = item.stackSize.ToString();
                }
                else
                {
                    itemText.text = "";
                }
            }
        }

        /// <summary>
        /// ���UI������Ʒ��
        /// </summary>
        public void CleanUpSlot()
        {
            item = null;

            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemText.text = "";
        }


        /// <summary>
        /// ���Ե�������ͼ�������ﴩ��װ����
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            EquipmentInventoryManager.instance?.EquipItem(item.itemData);

        }
    }
}