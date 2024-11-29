using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Demo.IMGUI
{
    [CustomPropertyDrawer(typeof(Ingredient))]
    public class IngredientDrawer : PropertyDrawer
    {
        // �ڸ����ľ����ڻ�������
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // �ڸ�������ʹ�� BeginProperty/EndProperty ��ζ��
            // Ԥ�Ƽ���д�߼��������������ԡ�
            EditorGUI.BeginProperty(position, label, property);

            //���Ʊ�ǩ
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // ��Ҫ�����ֶ�����
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // �������
            var amountRect = new Rect(position.x, position.y, 30, position.height);
            var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
            var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);

            // �����ֶ� - �� GUIContent.none ����ÿ���ֶΣ��Ӷ����Բ�ʹ�ñ�ǩ�������ֶ�
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
            EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("unit"), GUIContent.none);
            EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);

            // �������ָ�ԭ��
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}