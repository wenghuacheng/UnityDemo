using Demo.Common.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// ���UI
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance;

        [Header("Config")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private InventorySlot slotPrefab;//������۵�UI
        [SerializeField] private Transform container;

        [Header("Description Panel")]
        [SerializeField] private GameObject descriptionPanel;
        [SerializeField] private Image itemIcon;
        [SerializeField] private TextMeshProUGUI itemNameTMP;
        [SerializeField] private TextMeshProUGUI itemDescriptionTMP;

        public InventorySlot CurrentSlot { get; set; }//��ǰѡ�е�Slot

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
        /// ��ʼ���������
        /// </summary>
        private void InitInventory()
        {
            for (int i = 0; i < Inventory.Instance.InventorySize; i++)
            {
                var slot = Instantiate(slotPrefab, container);
                slot.Index = i;
                slotList.Add(slot);
                slot.ShowSlotInformation(false);//Ĭ����Ʒ�ǿյģ�ˢ��UIΪ��
            }
        }

        /// <summary>
        /// ʹ�õ�ǰ��Ʒ
        /// </summary>
        public void UseItem()
        {
            if (CurrentSlot == null) return;
            Inventory.Instance.UseItem(CurrentSlot.Index);
        }

        /// <summary>
        /// �Ƴ���Ʒ
        /// </summary>
        public void RemoveItem()
        {
            if (CurrentSlot == null) return;
            Inventory.Instance.RemoveItem(CurrentSlot.Index);
        }

        /// <summary>
        /// װ��
        /// </summary>
        public void EquipItem()
        {
            if (CurrentSlot == null) return;
            Inventory.Instance.EquipItem(CurrentSlot.Index);
        }

        /// <summary>
        /// ��ʾ�����²����Ϣ
        /// </summary>
        public void DrawItem(InventoryItem item, int index)
        {
            var slot = slotList[index];
            if (item == null)
            {
                //����Ʒ��������Ʒ��
                slot.ShowSlotInformation(false);
                return;
            }
            slot.ShowSlotInformation(true);
            slot.UpdateSlot(item);
        }

        /// <summary>
        /// ��ʾ��Ʒ����
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
        /// ��/�ر���塾���ﲻ�ᱻ���á�
        /// </summary>
        public void OpenCloseInventory()
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}