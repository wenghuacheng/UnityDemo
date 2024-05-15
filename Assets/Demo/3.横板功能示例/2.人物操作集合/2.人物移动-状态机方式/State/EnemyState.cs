using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public abstract class EnemyState : IState
    {
        //动画触发标记
        protected bool animatorTriggerFlag = false;
        //时间标记【用于一些定时执行的状态】
        protected float costTime = 0f;

        public virtual void Enter()
        {
            animatorTriggerFlag = false;
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            costTime -= Time.deltaTime;
            TransitionState();
        }

        public void AnimatorTrigger()
        {
            animatorTriggerFlag = true;
        }

        public abstract bool TransitionState();
    }
}