using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Weapons
{
    /// <summary>
    /// ��������
    /// </summary>
    [CreateAssetMenu(fileName = "WeaponDetailSO", menuName = "����ϵͳ/WeaponDetailSO")]
    public class WeaponDetailSO : ScriptableObject
    {
        public string weaponName;

        public Sprite weaponIcon;

        //��ҩ����λ��
        public Vector2 weaponShootPosition;

        //��ǰ����ʹ�õ�ҩ
        public AmmoDetailSO weaponCurrentAmmo;

        //�Ƿ������޵�ҩ��һ������������޵�ҩ������������Ҫ������ҩ��
        public bool hasInfiniteAmmo = false;

        //�Ƿ񵯼�����������һ�������������������������Ҫ������
        public bool hasInfiniteClipCapacity = false;

        //��������
        public int weaponClipAmmoCapacity = 30;

        //���ҩ���������ǿ��ϵͳ��
        public int weaponAmmoCapacity = 100;

        //�����������ʣ�0.2����0.2�뷢��һ�Σ�
        public float weaponFireRate = 0.2f;

        //��������ʱ�䣨����������������Ҫ��סX�����ܷ��䣩
        public float weaponPrechargeTime = 0f;

        //��������װ��ʱ��
        public float weaponReloadTime = 0f;
    }
}