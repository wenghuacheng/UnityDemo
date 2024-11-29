using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    /// <summary>
    /// 自定义属性
    /// （演示一个Range属性的显示）
    /// </summary>
    public class CustomRangeAttribute : PropertyAttribute
    {
        public readonly float min;
        public readonly float max;

        public CustomRangeAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}

