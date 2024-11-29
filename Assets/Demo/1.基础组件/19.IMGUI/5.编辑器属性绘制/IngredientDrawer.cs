using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo.IMGUI
{
    [CustomPropertyDrawer(typeof(Ingredient))]
    public class IngredientDrawer : PropertyDrawer
    {
        // 在给定的矩形内绘制属性
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // 在父属性上使用 BeginProperty/EndProperty 意味着
            // 预制件重写逻辑作用于整个属性。
            EditorGUI.BeginProperty(position, label, property);

            //绘制标签
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // 不要让子字段缩进
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // 计算矩形
            var amountRect = new Rect(position.x, position.y, 30, position.height);
            var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
            var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // 绘制字段 - 将 GUIContent.none 传入每个字段，从而可以不使用标签来绘制字段
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
            EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

            // 将缩进恢复原样
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}