using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// 状态变换参数
    /// </summary>
    [Serializable]
    public class FSMTransition
    {
        public FSMDecision Decision;//切换的判断逻辑
        public string TrueState;//符合条件时切换的状态ID
        public string FalseState;//不符合条件时切换的状态ID
    }
}