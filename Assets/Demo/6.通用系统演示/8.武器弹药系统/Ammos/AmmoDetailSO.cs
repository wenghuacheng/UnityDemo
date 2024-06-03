using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Weapons
{
    /// <summary>
    /// 弹药配置
    /// </summary>
    [CreateAssetMenu(fileName = "AmmoDetailSO", menuName = "武器系统/AmmoDetailSO")]
    public class AmmoDetailSO : ScriptableObject
    {
        public string ammoName;
        public bool isPlayerAmmo;

        public Sprite ammoSprite;

        //弹药预制体（通常只有一个，但是如果需要不同的效果可以创建多个）
        public GameObject[] ammoPrefabArray;

        //弹药材质
        public Material ammoMaterial;

        //弹药充能时间（一般敌人的所有弹药都需要改时间，这样在发射前会有一个进度，让玩家知道敌人马上要发射弹药了）
        public float ammoChargeTime = 0.1f;

        //弹药充能材质
        public Material ammoChargeMaterial;

        //弹药伤害
        public int ammoDamage = 1;

        //弹药移动速度（这里是固定的，可以定义minSpeed和maxSpeed两个属性让速度在这个范围之间生成）
        public float ammoSpeed = 20;

        //攻击范围
        public float ammoRange = 20;

        //弹药飞行中的旋转速度
        public float ammoRotationSpeed = 1;

        //弹药散步范围
        public float ammoSpreadMin = 0f;
        public float ammoSpreadMax = 1f;

        //一次发射弹药的数量（如霰弹枪一次发射多颗子弹，一般情况都是一颗）
        public int ammoSpawnAmountMin = 1;
        public int ammoSpawnAmountMax = 1;

        //弹药轨迹（如激化都是通过轨迹实现）
        public bool isAmmoTrail = false;

        // 轨迹显示时间
        public float ammoTrailTime = 3f;
        //轨迹材质
        public Material ammoTrailMaterial;
        //轨迹宽度
        [Range(0, 1)] public float ammoTrailStartWidth;
        //轨迹宽度
        [Range(0, 1)] public float ammoTrailEndWidth;
    }
}