using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.Ability
{
    /// <summary>
    /// 玩家动画触发器
    /// </summary>
    public class PlayerAnimationEventTrigger : MonoBehaviour
    {
        [SerializeField] private RPGPlayerAttackController attackController;

        /// <summary>
        /// 由帧事件触发
        /// </summary>
        public void OnAttackEndTrigger()
        {
            attackController.AttackOver();
        }
    }
}