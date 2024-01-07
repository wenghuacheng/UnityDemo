using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// 可伤害接口【生命值系统】
    /// </summary>
    public interface IDamageable 
    {
        void TakeDamage(float amount);
    }
}