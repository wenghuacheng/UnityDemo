using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ״̬�任����
    /// </summary>
    [Serializable]
    public class FSMTransition
    {
        public FSMDecision Decision;//�л����ж��߼�
        public string TrueState;//��������ʱ�л���״̬ID
        public string FalseState;//����������ʱ�л���״̬ID
    }
}