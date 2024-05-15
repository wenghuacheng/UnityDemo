using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    /// <summary>
    /// ���ܻ���
    /// </summary>
    public abstract class Skill : MonoBehaviour
    {
        //��ǰʱ��
        [SerializeField] protected float coolDown;
        //ʣ����ȴʱ��
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
