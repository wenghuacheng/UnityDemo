using Demo.Common.EnemySysWithUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// 玩家攻击
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Weapon initialWeapon;
        [SerializeField] private PlayerStats stat;
        [SerializeField] private Transform[] attackPositions;//位置需要逆时针摆放
        [SerializeField] private float minDistanceMeleeAttack = 3;//最大攻击距离

        private PlayerAnimations playerAnimations;
        private PlayerMana playerMana;
        private PlayerMovement playerMovement;
        private PlayerActions actions;
        private Coroutine curCoroutine;
        private EnemyBrain curEnemy;

        private Transform currentAttackPosition;//当前攻击位置
        private float currentAttackRotation;//当前位置需要旋转的角度

        private void Awake()
        {
            playerAnimations = GetComponent<PlayerAnimations>();
            playerMovement = GetComponent<PlayerMovement>();
            playerMana = GetComponent<PlayerMana>();
            actions = new PlayerActions();

            //默认方向
            currentAttackPosition = attackPositions[2];
            currentAttackRotation = -180;

            //装备默认装备
            EquipWeapon(initialWeapon);
        }

        private void Update()
        {
            GetFirePosition();
        }

        private void OnEnable()
        {
            actions.Enable();
            actions.Attack.PlayerAttack.performed += PerformedPlayerAttack;

            EnemySelectorManager.OnEnemySelected += EnemySelectorManager_OnEnemySelected;
            EnemySelectorManager.OnEnemyUnSelected += EnemySelectorManager_OnEnemyUnSelected;
        }

        private void OnDisable()
        {
            actions.Disable();
            actions.Attack.PlayerAttack.performed -= PerformedPlayerAttack;

            EnemySelectorManager.OnEnemySelected -= EnemySelectorManager_OnEnemySelected;
            EnemySelectorManager.OnEnemyUnSelected -= EnemySelectorManager_OnEnemyUnSelected;
        }

        /// <summary>
        /// 攻击按键
        /// </summary>
        /// <param name="obj"></param>
        private void PerformedPlayerAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Attack();
        }

        /// <summary>
        /// 攻击
        /// </summary>
        private void Attack()
        {
            if (curCoroutine != null)
            {
                StopCoroutine(curCoroutine);
            }
            curCoroutine = StartCoroutine(AsyncAttack());
        }

        /// <summary>
        /// 攻击
        /// </summary>
        private IEnumerator AsyncAttack()
        {
            if (initialWeapon.WeaponType == WeaponType.Melee)
            {
                MeleeAttack();
            }
            else if (initialWeapon.WeaponType == WeaponType.Magic)
            {
                MagicAttack();
            }

            //播放动画
            playerAnimations.SetAttackingAnimation(true);
            yield return new WaitForSeconds(0.5f);
            playerAnimations.SetAttackingAnimation(false);
        }

        /// <summary>
        /// 近战攻击
        /// </summary>
        private void MeleeAttack()
        {
            if (curEnemy == null) return;
            float enemeyDistance = Vector3.Distance(curEnemy.transform.position, this.transform.position);
            if (enemeyDistance < minDistanceMeleeAttack)
            {
                curEnemy.GetComponent<IDamageable>().TakeDamage(GetAttackDamage());
            }
        }

        /// <summary>
        /// 魔法攻击【远程】
        /// </summary>
        private void MagicAttack()
        {
            //发射弹药
            if (currentAttackPosition != null)
            {
                if (playerMana.CurrentMana < initialWeapon.RequiredMana) return;

                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, currentAttackRotation));
                var projectile = Instantiate(initialWeapon.ProjectilePrefab, currentAttackPosition.position, rotation);
                projectile.Direction = Vector3.up;//使用Translate，所以旋转后直接向着方向移动
                projectile.Damage = GetAttackDamage();//伤害值
                projectile.Owner = this.gameObject;

                //消耗魔法值
                playerMana.UseMana(initialWeapon.RequiredMana);
            }
        }

        /// <summary>
        /// 获取发射位置
        /// </summary>
        private void GetFirePosition()
        {
            var direction = playerMovement.MoveDirection;
            switch (direction.x)
            {
                case > 0://向右
                    currentAttackPosition = attackPositions[1];
                    currentAttackRotation = -90;
                    break;
                case < 0://向左
                    currentAttackPosition = attackPositions[3];
                    currentAttackRotation = -270;
                    break;
            }

            switch (direction.y)
            {
                case > 0://向上
                    currentAttackPosition = attackPositions[0];
                    currentAttackRotation = 0;
                    break;
                case < 0://向下
                    currentAttackPosition = attackPositions[2];
                    currentAttackRotation = -180;
                    break;
            }
        }

        /// <summary>
        /// 添加装备
        /// </summary>
        public void EquipWeapon(Weapon newWeapon)
        {
            initialWeapon = newWeapon;
            stat.TotalDamage = stat.BaseDamage + (newWeapon == null ? 0 : newWeapon.Damage);
        }

        /// <summary>
        /// 获取攻击伤害
        /// </summary>
        /// <returns></returns>
        private float GetAttackDamage()
        {
            float damage = stat.BaseDamage;//基础伤害
            damage = initialWeapon.Damage;//武器伤害
            float randomPerc = Random.Range(0, 100);
            if (randomPerc <= stat.CriticalChance)
            {
                damage += damage * (stat.CriticalDamage / 100f);//致命伤害
            }
            return damage;
        }

        /// <summary>
        /// 选择敌人
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void EnemySelectorManager_OnEnemySelected(EnemyBrain obj)
        {
            this.curEnemy = obj;
        }
        private void EnemySelectorManager_OnEnemyUnSelected()
        {
            this.curEnemy = null;
        }

    }
}