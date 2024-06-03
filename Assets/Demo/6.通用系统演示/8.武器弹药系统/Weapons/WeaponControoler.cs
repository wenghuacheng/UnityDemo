using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Weapons
{
    /// <summary>
    /// 武器管理【可以写在player类中】
    /// </summary>
    public class WeaponControoler : MonoBehaviour
    {
        [SerializeField] private WeaponDetailSO[] weaponDetails;

        public List<Weapon> weaponList { get; set; } = new List<Weapon>();
        private int currentWeaponIndex;


        private void Start()
        {
            weaponList.Clear();
            for (int i = 0; i < weaponDetails.Length; i++)
            {
                var weapon = Weapon.CreateNew(weaponDetails[i]);
                weaponList.Add(weapon);
                weapon.weaponListPosition = i;//设置索引
            }

            SetStartingWeapon();
        }


        /// <summary>
        /// 设置起始武器
        /// </summary>
        private void SetStartingWeapon()
        {
            int currentWeaponIndex = 0;

            //todo：触发事件通知如UI刷新
        }
    }
}