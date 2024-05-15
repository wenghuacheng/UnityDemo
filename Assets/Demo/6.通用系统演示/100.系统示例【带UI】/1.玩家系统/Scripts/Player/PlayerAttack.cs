using Demo.Common.EnemySysWithUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// ��ҹ���
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Weapon initialWeapon;
        [SerializeField] private PlayerStats stat;
        [SerializeField] private Transform[] attackPositions;//λ����Ҫ��ʱ��ڷ�
        [SerializeField] private float minDistanceMeleeAttack = 3;//��󹥻�����

        private PlayerAnimations playerAnimations;
        private PlayerMana playerMana;
        private PlayerMovement playerMovement;
        private PlayerActions actions;
        private Coroutine curCoroutine;
        private EnemyBrain curEnemy;

        private Transform currentAttackPosition;//��ǰ����λ��
        private float currentAttackRotation;//��ǰλ����Ҫ��ת�ĽǶ�

        private void Awake()
        {
            playerAnimations = GetComponent<PlayerAnimations>();
            playerMovement = GetComponent<PlayerMovement>();
            playerMana = GetComponent<PlayerMana>();
            actions = new PlayerActions();

            //Ĭ�Ϸ���
            currentAttackPosition = attackPositions[2];
            currentAttackRotation = -180;

            //װ��Ĭ��װ��
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
        /// ��������
        /// </summary>
        /// <param name="obj"></param>
        private void PerformedPlayerAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Attack();
        }

        /// <summary>
        /// ����
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
        /// ����
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

            //���Ŷ���
            playerAnimations.SetAttackingAnimation(true);
            yield return new WaitForSeconds(0.5f);
            playerAnimations.SetAttackingAnimation(false);
        }

        /// <summary>
        /// ��ս����
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
        /// ħ��������Զ�̡�
        /// </summary>
        private void MagicAttack()
        {
            //���䵯ҩ
            if (currentAttackPosition != null)
            {
                if (playerMana.CurrentMana < initialWeapon.RequiredMana) return;

                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, currentAttackRotation));
                var projectile = Instantiate(initialWeapon.ProjectilePrefab, currentAttackPosition.position, rotation);
                projectile.Direction = Vector3.up;//ʹ��Translate��������ת��ֱ�����ŷ����ƶ�
                projectile.Damage = GetAttackDamage();//�˺�ֵ
                projectile.Owner = this.gameObject;

                //����ħ��ֵ
                playerMana.UseMana(initialWeapon.RequiredMana);
            }
        }

        /// <summary>
        /// ��ȡ����λ��
        /// </summary>
        private void GetFirePosition()
        {
            var direction = playerMovement.MoveDirection;
            switch (direction.x)
            {
                case > 0://����
                    currentAttackPosition = attackPositions[1];
                    currentAttackRotation = -90;
                    break;
                case < 0://����
                    currentAttackPosition = attackPositions[3];
                    currentAttackRotation = -270;
                    break;
            }

            switch (direction.y)
            {
                case > 0://����
                    currentAttackPosition = attackPositions[0];
                    currentAttackRotation = 0;
                    break;
                case < 0://����
                    currentAttackPosition = attackPositions[2];
                    currentAttackRotation = -180;
                    break;
            }
        }

        /// <summary>
        /// ���װ��
        /// </summary>
        public void EquipWeapon(Weapon newWeapon)
        {
            initialWeapon = newWeapon;
            stat.TotalDamage = stat.BaseDamage + (newWeapon == null ? 0 : newWeapon.Damage);
        }

        /// <summary>
        /// ��ȡ�����˺�
        /// </summary>
        /// <returns></returns>
        private float GetAttackDamage()
        {
            float damage = stat.BaseDamage;//�����˺�
            damage = initialWeapon.Damage;//�����˺�
            float randomPerc = Random.Range(0, 100);
            if (randomPerc <= stat.CriticalChance)
            {
                damage += damage * (stat.CriticalDamage / 100f);//�����˺�
            }
            return damage;
        }

        /// <summary>
        /// ѡ�����
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