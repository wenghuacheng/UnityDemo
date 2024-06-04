using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Demo.Common.Weapons
{
    /// <summary>
    /// 当前武器
    /// </summary>
    public class ActiveWeapon : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer weaponSpriteRenderer;
        [SerializeField] protected PolygonCollider2D weaponPolygonCollider;
        [SerializeField] public Transform weaponShootPositionTransform;
        [SerializeField] private Transform weaponEffectPositionTransform;

        public Weapon currentWeapon;

        /// <summary>
        /// PS:可以通过事件触发
        /// </summary>
        public void SetWeapon(Weapon weapon)
        {
            currentWeapon = weapon;

            weaponSpriteRenderer.sprite = weapon.weaponDetail.weaponIcon;

            //通过sprite中的点设置多边形碰撞体
            if (weaponPolygonCollider != null && weaponSpriteRenderer.sprite != null)
            {
                List<Vector2> physicsShapePointList = new List<Vector2>();
                weaponSpriteRenderer.sprite.GetPhysicsShape(0, physicsShapePointList);
                weaponPolygonCollider.points = physicsShapePointList.ToArray();
            }

            //设置发射位置（需要先在Scene视窗中测试出点的位置，然后设置入detail中）
            weaponShootPositionTransform.localPosition = currentWeapon.weaponDetail.weaponShootPosition;

        }
    }

    //开火事件
    public class FireWeaponEvent
    {
        public bool fire;

        public float aimAngle;
        public float weaponAimAngle;
        public Vector2 weaponAimDirectionVector;
    }

    public class FireWeapon : MonoBehaviour
    {
        private ActiveWeapon activeWeapon;

        private float fireRateCooldownTimer = 0;//冷却时间

        private void Awake()
        {
            activeWeapon = GetComponent<ActiveWeapon>();
        }

        private void Update()
        {
            fireRateCooldownTimer -= Time.deltaTime;
        }

        /// <summary>
        /// 开火事件处理
        /// </summary>
        /// <param name="event"></param>
        private void OnFireWeapon(FireWeaponEvent @event)
        {
            if (IsWeaponReadyToFire())
            {
                FireAmmo(@event.aimAngle, @event.weaponAimAngle, @event.weaponAimDirectionVector);
                //重置冷却时间
                fireRateCooldownTimer = activeWeapon.currentWeapon.weaponDetail.weaponFireRate;
            }
        }

        private bool IsWeaponReadyToFire()
        {
            //弹夹弹药量检测
            if (activeWeapon.currentWeapon.weaponRemainAmmo <= 0 && !activeWeapon.currentWeapon.weaponDetail.hasInfiniteAmmo)
                return false;

            //是否正在重新装填
            if (activeWeapon.currentWeapon.isWeaponReloading)
                return false;

            if (fireRateCooldownTimer > 0)
                return false;

            //剩余弹药检测
            if (activeWeapon.currentWeapon.weaponClipRemainingAmmo <= 0 && !activeWeapon.currentWeapon.weaponDetail.hasInfiniteClipCapacity)
                return false;
            return true;
        }

        private void FireAmmo(float aimAngle, float weaponAimAngle, Vector2 weaponAimDirectionVector)
        {
            var ammoDetail = activeWeapon.currentWeapon.weaponDetail.weaponCurrentAmmo;
            //随机生成弹药
            var ammoPrefab = ammoDetail.ammoPrefabArray[UnityEngine.Random.Range(0, ammoDetail.ammoPrefabArray.Length)];
            //生成弹药（使用对象池）
            var ammoObj = Instantiate(ammoPrefab, activeWeapon.weaponShootPositionTransform.position, Quaternion.identity);

            var ammo = ammoObj.GetComponent<IFireable>();
            ammo.InitializeAmmo(ammoDetail, aimAngle, weaponAimAngle, ammoDetail.ammoSpeed, weaponAimDirectionVector);

            if (!activeWeapon.currentWeapon.weaponDetail.hasInfiniteClipCapacity)
            {
                activeWeapon.currentWeapon.weaponClipRemainingAmmo--;
                activeWeapon.currentWeapon.weaponRemainAmmo--;
            }


        }
    }

    public class WeaponReload : MonoBehaviour
    {
        private Coroutine coroutine;

        //需要判断是否可以进行装填

        public void StartReloading()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine("Reload");
        }

        private IEnumerator Reload(Weapon weapon, int reloadAmmoPercent)
        {
            weapon.isWeaponReloading = true;

            while (weapon.weaponReloadTimer < weapon.weaponDetail.weaponReloadTime)
            {
                weapon.weaponReloadTimer += Time.deltaTime;
                yield return null;
            }

            if (reloadAmmoPercent != 0)
            {
                //计算需要安装的弹药数量【reloadAmmoPercent=50表示装弹50%】
                int ammoIncrease = Mathf.RoundToInt(weapon.weaponDetail.weaponAmmoCapacity * reloadAmmoPercent / 100f);

                int totalAmmo = weapon.weaponRemainAmmo + ammoIncrease;

                if (totalAmmo > weapon.weaponDetail.weaponAmmoCapacity)
                {
                    //不能大于弹夹数量
                    weapon.weaponRemainAmmo = weapon.weaponDetail.weaponAmmoCapacity;
                }
                else
                {
                    weapon.weaponRemainAmmo = totalAmmo;
                }
            }


            if (weapon.weaponDetail.hasInfiniteAmmo)
            {
                //无限弹药
                weapon.weaponClipRemainingAmmo = weapon.weaponDetail.weaponClipAmmoCapacity;
            }
            else if (weapon.weaponRemainAmmo >= weapon.weaponDetail.weaponClipAmmoCapacity)
            {
                //剩余总弹药
                weapon.weaponClipRemainingAmmo = weapon.weaponDetail.weaponClipAmmoCapacity;
            }
            else
            {
                //弹药不足，全部装入弹夹
                weapon.weaponClipRemainingAmmo = weapon.weaponRemainAmmo;
            }

            weapon.weaponReloadTimer = 0;
            weapon.isWeaponReloading = false;

            //todo:触发事件，装弹完成
        }
    }
}
