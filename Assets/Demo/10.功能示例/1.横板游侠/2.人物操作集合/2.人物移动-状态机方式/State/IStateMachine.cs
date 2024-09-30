using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

namespace HB.Operation.State
{
    public abstract class IStateMachine<T1, T2>
        where T1 : Enum
        where T2 : IState
    {
        //��ǰ״̬
        public T2 CurrentState { get; protected set; }

        /// <summary>
        /// ��ʼ��״̬
        /// </summary>
        public virtual void Initialize()
        {
            CurrentState?.Enter();
        }

        /// <summary>
        /// ״̬���
        /// ������ͨ�������Ż�������߼���
        /// </summary>
        public virtual void ChangeState(T1 state)
        {
            CurrentState?.Exit();
            CurrentState = InternalChangeState(state);
            CurrentState?.Enter();
        }

        /// <summary>
        /// ���µ�ǰ״̬
        /// </summary>
        public virtual void Update()
        {
            CurrentState?.Update();
        }


        //״̬ת��
        protected abstract T2 InternalChangeState(T1 state);

    }
}
