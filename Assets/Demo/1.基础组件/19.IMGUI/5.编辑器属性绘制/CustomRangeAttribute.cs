using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.IMGUI
{
    /// <summary>
    /// �Զ�������
    /// ����ʾһ��Range���Ե���ʾ��
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

