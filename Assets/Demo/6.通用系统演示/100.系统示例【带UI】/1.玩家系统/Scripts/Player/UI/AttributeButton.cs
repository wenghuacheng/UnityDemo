using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.PlayerSysWithUI
{
    /// <summary>
    /// �������ֵ��ť
    /// </summary>
    public class AttributeButton : MonoBehaviour
    {
        public static event Action<AttributeType> OnAttributeSelectedEvent;

        [Header("Config")]
        [SerializeField] private AttributeType attribute;

        public void SelectAttribute()
        {
            OnAttributeSelectedEvent?.Invoke(attribute);
        }
    }
}