using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public abstract class EnemyState : IState
    {
        //�����������
        protected bool animatorTriggerFlag = false;
        //ʱ���ǡ�����һЩ��ʱִ�е�״̬��
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