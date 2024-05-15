using Demo.Common.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// 库存UI
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance;

        [Header("Config")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private InventorySlot slotPrefab;//单个插槽的UI
        [SerializeField] private Transform container;

        [Header("Description Panel")]
        [SerializeField] private GameObject descriptionPanel;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemNameTMP;
        [SerializeField] private TextMeshProUGUI itemDescriptionTMP;

        public InventorySlot CurrentSlot { get; set; }//当前选中的Slot

        private List<InventorySlot> slotList = new List<InventorySlot>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            InitInventory();
            descriptionPanel.SetActive(false);
        }

        private void OnEnable()
        {
            InventorySlot.OnSlotSelectedEvent += InventorySlot_OnSlotSelectedEvent;
        }

        private void OnDisable()
        {
            InventorySlot.OnSlotSelectedEvent -= InventorySlot_OnSlotSelectedEvent;
        }

        private void InventorySlot_OnSlotSelectedEvent(int index)
        {
            CurrentSlot = slotList[index];
            ShowItemDescription(index);
        }

        /// <summary>
        /// 初始化库存容器
        /// </summary>
        private void InitInventory()
        {
            for (int i = 0; i < Inventory.Instance.InventorySize; i++)
            {
                var slot = Instantiate(slotPrefab, container);
                slot.Index = i;
                slotList.Add(slot);
                slot.ShowSlotInformation(false);//默认物品是空的，刷新UI为空
            }
        }

        /// <summary>
        /// 使用当前物品
        /// </summary>
        public void UseItem()
        {
            if (CurrentSlot == null) return;
            Inventory.Instance.UseItem(CurrentSlot.Index);
        }

        /// <summary>
        /// 移除物品
        /// </summary>
        public void RemoveItem()
        {
            if (CurrentSlot == null) return;
            Inventory.Instance.RemoveItem(CurrentSlot.Index);
        }

        /// <summary>
        /// 装备
        /// </summary>
        public void EquipItem()
        {
            if (CurrentSlot == null) return;
            Inventory.Instance.EquipItem(CurrentSlot.Index);
        }

        /// <summary>
        /// 显示并更新插槽信息
        /// </summary>
        public void DrawItem(InventoryItem item, int index)
        {
            var slot = slotList[index];
            if (item == null)
            {
                //无物品则隐藏物品栏
                slot.ShowSlotInformation(false);
                return;
            }
            slot.ShowSlotInformation(true);
            slot.UpdateSlot(item);
        }

        /// <summary>
        /// 显示物品描述
        /// </summary>
        /// <param name="index"></param>
        public void ShowItemDescription(int index)
        {
            if (Inventory.Instance.InventoryItems[index] == null) return;
            descriptionPanel.SetActive(true);
            itemIcon.sprite = Inventory.Instance.InventoryItems[index].Icon;
            itemNameTMP.text= Inventory.Instance.InventoryItems[index].Name;
            itemDescriptionTMP.text = Inventory.Instance.InventoryItems[index].Description;

        }

        /// <summary>
        /// 打开/关闭面板【这里不会被调用】
        /// </summary>
        public void OpenCloseInventory()
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}