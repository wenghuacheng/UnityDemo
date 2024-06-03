using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Weapons
{
    /// <summary>
    /// 武器配置
    /// </summary>
    [CreateAssetMenu(fileName = "WeaponDetailSO", menuName = "武器系统/WeaponDetailSO")]
    public class WeaponDetailSO : ScriptableObject
    {
        public string weaponName;

        public Sprite weaponIcon;

        //弹药发射位置
        public Vector2 weaponShootPosition;

        //当前武器使用弹药
        public AmmoDetailSO weaponCurrentAmmo;

        //是否是无限弹药（一般基础武器无限弹药，其他武器需要供给弹药）
        public bool hasInfiniteAmmo = false;

        //是否弹夹无限容量（一般敌人是无限容量，这样不需要换弹）
        public bool hasInfiniteClipCapacity = false;

        //弹夹容量
        public int weaponClipAmmoCapacity = 30;

        //最大弹药持有量（非库存系统）
        public int weaponAmmoCapacity = 100;

        //武器发射速率（0.2代表0.2秒发射一次）
        public float weaponFireRate = 0.2f;

        //武器充能时间（类似蓄力攻击，需要按住X秒后才能发射）
        public float weaponPrechargeTime = 0f;

        //武器重新装填时间
        public float weaponReloadTime = 0f;
    }
}