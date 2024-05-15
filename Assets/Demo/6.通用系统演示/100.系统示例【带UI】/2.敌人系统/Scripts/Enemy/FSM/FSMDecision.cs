using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.EnemySysWithUI
{
    /// <summary>
    /// ×´Ì¬µÄ×´Ì¬ÇÐ»»ÅÐ¶Ï
    /// </summary>
    public abstract class FSMDecision : MonoBehaviour
    {
        public abstract bool Decide();
    }
}