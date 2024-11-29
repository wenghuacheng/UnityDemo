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
        // �ڸ����ľ����ڻ�������
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //���Ȼ�ȡ�����ԣ���Ϊ�������������ķ�Χ
            CustomRangeAttribute range = (CustomRangeAttribute)attribute;

            // ���ڸ��������Ǹ���ֵ����������ȷ�������Ի���Ϊ Slider ���� IntSlider��
            if (property.propertyType == SerializedPropertyType.Float)
                EditorGUI.Slider(position, property, range.min, range.max, label);
            else if (property.propertyType == SerializedPropertyType.Integer)
                EditorGUI.IntSlider(position, property, (int)range.min, (int)range.max, label);
            else
                EditorGUI.LabelField(position, label.text, "Use MyRange with float or int.");
        }
    }
}