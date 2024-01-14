using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// UI中单个单元格
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
        /// 点击该按钮
        /// </summary>
        public void ClickSlot()
        {
            OnSlotSelectedEvent?.Invoke(Index);
        }


        /// <summary>
        /// 更新UI界面数据
        /// </summary>
        public void UpdateSlot(InventoryItem item)
        {
            itemIcon.sprite = item.Icon;
            itemQuantityTMP.text = item.Quantity.ToString();
        }

        /// <summary>
        /// 单个插槽是否显示
        /// </summary>
        /// <param name="value"></param>
        public void ShowSlotInformation(bool value)
        {
            itemIcon.gameObject.SetActive(value);
            quantityContainer.gameObject.SetActive(value);


        }
    }
}