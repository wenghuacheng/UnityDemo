using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    /// <summary>
    /// 技能基类
    /// </summary>
    public abstract class Skill : MonoBehaviour
    {
        //当前时间
        [SerializeField] protected float coolDown;
        //剩余冷却时间
        protected float coolDownTime;


        protected virtual void Update()
        {
            coolDownTime -= Time.deltaTime;
        }

        public virtual bool CanUseSkill()
        {
            if (coolDownTime < 0)
            {
                coolDownTime = coolDown;
                return true;
            }
            return false;
        }

        public virtual void UseSkill()
        {

        }

    }
}
