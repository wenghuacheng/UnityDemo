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
        //当前状态
        public T2 CurrentState { get; protected set; }

        /// <summary>
        /// 初始化状态
        /// </summary>
        public virtual void Initialize()
        {
            CurrentState?.Enter();
        }

        /// <summary>
        /// 状态变更
        /// 【可以通过集合优化这里的逻辑】
        /// </summary>
        public virtual void ChangeState(T1 state)
        {
            CurrentState?.Exit();
            CurrentState = InternalChangeState(state);
            CurrentState?.Enter();
        }

        /// <summary>
        /// 更新当前状态
        /// </summary>
        public virtual void Update()
        {
            CurrentState?.Update();
        }


        //状态转换
        protected abstract T2 InternalChangeState(T1 state);

    }
}
