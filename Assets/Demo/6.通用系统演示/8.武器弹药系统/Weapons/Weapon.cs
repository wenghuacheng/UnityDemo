using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Weapons
{
    /// <summary>
    /// ��¼��ǰ��������
    /// </summary>
    public class Weapon
    {
        public WeaponDetailSO weaponDetail;
        //��ǰʹ���������б��е�����
        public int weaponListPosition;
        //ʣ��װ��ʱ��
        public float weaponReloadTimer;
        //��ǰ������ʣ�൯ҩ��
        public int weaponClipRemainingAmmo;
        //��ǰ����ʣ�൯ҩ����
        public int weaponRemainAmmo;
        //��ǰ�Ƿ�����װ��
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