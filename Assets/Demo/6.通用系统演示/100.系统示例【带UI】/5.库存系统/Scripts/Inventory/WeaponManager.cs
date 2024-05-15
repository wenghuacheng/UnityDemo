using Demo.Common.PlayerSysWithUI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Common.InventorySysWithUI
{
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager Instance;

        [Header("Config")]
        [SerializeField] private Image weaponIcon;
        [SerializeField] private TextMeshProUGUI weaponManaTMP;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// ˢ��װ��UI
        /// </summary>
        /// <param name="weapon"></param>
        public void EquipWeapon(Weapon weapon)
        {
            weaponIcon.sprite = weapon.Icon;
            weaponIcon.SetNativeSize();
            weaponIcon.gameObject.SetActive(true);
            weaponManaTMP.text = weapon.RequiredMana.ToString();
            weaponManaTMP.gameObject.SetActive(true);

            //todo:װ����PlayerҲҪˢ�¹�����ص���ֵ������û��player�Ͳ�Ū��
        }
    }
}