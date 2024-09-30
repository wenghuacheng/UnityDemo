using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.Ability
{
    /// <summary>
    /// ��Ҷ���������
    /// </summary>
    public class PlayerAnimationEventTrigger : MonoBehaviour
    {
        [SerializeField] private RPGPlayerAttackController attackController;

        /// <summary>
        /// ��֡�¼�����
        /// </summary>
        public void OnAttackEndTrigger()
        {
            attackController.AttackOver();
        }
    }
}