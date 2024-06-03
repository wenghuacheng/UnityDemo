using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Weapons
{
    /// <summary>
    /// ��ҩ����
    /// </summary>
    [CreateAssetMenu(fileName = "AmmoDetailSO", menuName = "����ϵͳ/AmmoDetailSO")]
    public class AmmoDetailSO : ScriptableObject
    {
        public string ammoName;
        public bool isPlayerAmmo;

        public Sprite ammoSprite;

        //��ҩԤ���壨ͨ��ֻ��һ�������������Ҫ��ͬ��Ч�����Դ��������
        public GameObject[] ammoPrefabArray;

        //��ҩ����
        public Material ammoMaterial;

        //��ҩ����ʱ�䣨һ����˵����е�ҩ����Ҫ��ʱ�䣬�����ڷ���ǰ����һ�����ȣ������֪����������Ҫ���䵯ҩ�ˣ�
        public float ammoChargeTime = 0.1f;

        //��ҩ���ܲ���
        public Material ammoChargeMaterial;

        //��ҩ�˺�
        public int ammoDamage = 1;

        //��ҩ�ƶ��ٶȣ������ǹ̶��ģ����Զ���minSpeed��maxSpeed�����������ٶ��������Χ֮�����ɣ�
        public float ammoSpeed = 20;

        //������Χ
        public float ammoRange = 20;

        //��ҩ�����е���ת�ٶ�
        public float ammoRotationSpeed = 1;

        //��ҩɢ����Χ
        public float ammoSpreadMin = 0f;
        public float ammoSpreadMax = 1f;

        //һ�η��䵯ҩ��������������ǹһ�η������ӵ���һ���������һ�ţ�
        public int ammoSpawnAmountMin = 1;
        public int ammoSpawnAmountMax = 1;

        //��ҩ�켣���缤������ͨ���켣ʵ�֣�
        public bool isAmmoTrail = false;

        // �켣��ʾʱ��
        public float ammoTrailTime = 3f;
        //�켣����
        public Material ammoTrailMaterial;
        //�켣���
        [Range(0, 1)] public float ammoTrailStartWidth;
        //�켣���
        [Range(0, 1)] public float ammoTrailEndWidth;
    }
}