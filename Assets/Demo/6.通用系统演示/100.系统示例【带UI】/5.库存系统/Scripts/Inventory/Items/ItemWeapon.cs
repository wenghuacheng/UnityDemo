using Demo.Common.PlayerSysWithUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.InventorySysWithUI
{
    /// <summary>
    /// 武器物品
    /// </summary>
    [CreateAssetMenu(fileName = "ItemWeapon_", menuName = "带UI演示/库存系统/ItemWeapon")]
    public class ItemWeapon : InventoryItem
    {
        [Header("Weapon")]
        public Weapon Weapon;

        public override void EquipItem()
        {
            //装备
            WeaponManager.Instance.EquipWeapon(Weapon);
        }
    }
}