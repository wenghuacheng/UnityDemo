using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Weapons
{
    /// <summary>
    /// ������������д��player���С�
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
                weapon.weaponListPosition = i;//��������
            }

            SetStartingWeapon();
        }


        /// <summary>
        /// ������ʼ����
        /// </summary>
        private void SetStartingWeapon()
        {
            int currentWeaponIndex = 0;

            //todo�������¼�֪ͨ��UIˢ��
        }
    }
}