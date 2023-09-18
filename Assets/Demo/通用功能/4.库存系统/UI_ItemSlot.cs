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
    /// 库存显示【单格物品的UI脚本】
    /// </summary>
    public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemText;

        public InventoryItem item;

        /// <summary>
        /// 刷新UI
        /// </summary>
        /// <param name="newItem"></param>
        public void UpdateSlot(InventoryItem newItem)
        {
            this.item = newItem;

            if (item != null)
            {
                itemImage.color = Color.white;//默认设置为不透明

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
        /// 清空UI【无物品】
        /// </summary>
        public void CleanUpSlot()
        {
            item = null;

            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemText.text = "";
        }


        /// <summary>
        /// 测试点击【点击图标后给人物穿上装备】
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            EquipmentInventoryManager.instance?.EquipItem(item.itemData);

        }
    }
}