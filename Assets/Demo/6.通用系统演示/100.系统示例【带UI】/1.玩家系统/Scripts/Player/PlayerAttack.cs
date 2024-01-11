using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// Íæ¼Ò¹¥»÷
    /// </summary>
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerAnimations playerAnimations;
        private PlayerActions actions;

        private Coroutine curCoroutine;

        private void Awake()
        {
            playerAnimations = GetComponent<PlayerAnimations>();
            actions = new PlayerActions();
        }

        private void OnEnable()
        {
            actions.Enable();
            actions.Attack.PlayerAttack.performed += PerformedPlayerAttack;
        }

        private void OnDisable()
        {
            actions.Disable();
            actions.Attack.PlayerAttack.performed -= PerformedPlayerAttack;
        }

        /// <summary>
        /// ¹¥»÷°´¼ü
        /// </summary>
        /// <param name="obj"></param>
        private void PerformedPlayerAttack(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Attack();
        }

        /// <summary>
        /// ¹¥»÷
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
        /// ¹¥»÷
        /// </summary>
        private IEnumerator AsyncAttack()
        {
            playerAnimations.SetAttackingAnimation(true);
            yield return new WaitForSeconds(0.5f);
            playerAnimations.SetAttackingAnimation(false);
        }
    }
}