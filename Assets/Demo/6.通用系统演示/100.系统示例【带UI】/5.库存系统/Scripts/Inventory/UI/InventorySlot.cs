using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// UI�е�����Ԫ��
    /// </summary>
    public class InventorySlot : MonoBehaviour
    {
        public static event Action<int> OnSlotSelectedEvent;

        [Header("Config")]
        [SerializeField] private Image itemIcon;
        [SerializeField] private Image quantityContainer;
        [SerializeField] private TextMeshProUGUI itemQuantityTMP;

        public int Index { get; set; }

        /// <summary>
        /// ����ð�ť
        /// </summary>
        public void ClickSlot()
        {
            OnSlotSelectedEvent?.Invoke(Index);
        }


        /// <summary>
        /// ����UI��������
        /// </summary>
        public void UpdateSlot(InventoryItem item)
        {
            itemIcon.sprite = item.Icon;
            itemQuantityTMP.text = item.Quantity.ToString();
        }

        /// <summary>
        /// ��������Ƿ���ʾ
        /// </summary>
        /// <param name="value"></param>
        public void ShowSlotInformation(bool value)
        {
            itemIcon.gameObject.SetActive(value);
            quantityContainer.gameObject.SetActive(value);


        }
    }
}