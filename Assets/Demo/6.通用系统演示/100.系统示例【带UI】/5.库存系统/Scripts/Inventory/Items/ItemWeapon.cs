using Demo.Common.PlayerSysWithUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// ������Ʒ
    /// </summary>
    [CreateAssetMenu(fileName = "ItemWeapon_", menuName = "��UI��ʾ/���ϵͳ/ItemWeapon")]
    public class ItemWeapon : InventoryItem
    {
        [Header("Weapon")]
        public Weapon Weapon;

        public override void EquipItem()
        {
            //װ��
            WeaponManager.Instance.EquipWeapon(Weapon);
        }
    }
}