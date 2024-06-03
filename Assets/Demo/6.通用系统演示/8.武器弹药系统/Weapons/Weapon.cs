using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Weapons
{
    /// <summary>
    /// 记录当前武器数据
    /// </summary>
    public class Weapon
    {
        public WeaponDetailSO weaponDetail;
        //当前使用武器在列表中的索引
        public int weaponListPosition;
        //剩余装填时间
        public float weaponReloadTimer;
        //当前弹夹中剩余弹药量
        public int weaponClipRemainingAmmo;
        //当前人物剩余弹药总量
        public int weaponRemainAmmo;
        //当前是否正在装填
        public bool isWeaponReloading;

        public static Weapon CreateNew(WeaponDetailSO weaponDetail)
        {
            Weapon weapon = new Weapon()
            {
                weaponDetail = weaponDetail,
                weaponReloadTimer = 0,
                weaponClipRemainingAmmo = weaponDetail.weaponClipAmmoCapacity,
                weaponRemainAmmo = weaponDetail.weaponAmmoCapacity,
                isWeaponReloading = false 
            };


            return weapon;
        }
    }
}