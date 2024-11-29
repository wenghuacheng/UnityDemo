using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo.IMGUI
{
    [CustomPropertyDrawer(typeof(CustomRangeAttribute))]
    public class CustomRangeDrawer : PropertyDrawer
    {
        // 在给定的矩形内绘制属性
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //首先获取该特性，因为它包含滑动条的范围
            CustomRangeAttribute range = (CustomRangeAttribute)attribute;

            // 现在根据属性是浮点值还是整数来确定将属性绘制为 Slider 还是 IntSlider。
            if (property.propertyType == SerializedPropertyType.Float)
                EditorGUI.Slider(position, property, range.min, range.max, label);
            else if (property.propertyType == SerializedPropertyType.Integer)
                EditorGUI.IntSlider(position, property, (int)range.min, (int)range.max, label);
            else
                EditorGUI.LabelField(position, label.text, "Use MyRange with float or int.");
        }
    }
}