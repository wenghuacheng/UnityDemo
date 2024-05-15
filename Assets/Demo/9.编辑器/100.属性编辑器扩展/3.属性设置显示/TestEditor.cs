using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Demo.CustomEditors
{
    [CustomEditor(typeof(TestHolder))]
    public class TestEditor : Editor
    {
        private TestHolder holder;

        private void OnEnable()
        {
            if (holder == null) holder = target as TestHolder;//从target中获取对象数据
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            ShowPopup();
            ShowEnumPopup();
            ShowLabelField();
            ShowTextField();
            ShowIntField();
            ShowFloatField();
            ShowToggle();
            ShowSlider();
            ShowColorField();
            ShowTextArea();
            ShowCurveField();
            ShowGradientField();
            ShowVector2Field();
            ShowBoundsField();
            ShowHelpBox();
            ShowLayerField();
            ShowTagField();
            ShowObjectField();
            ShowFoldout();
            GUIHorizontal();
            ShowPropertyField();

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 基于文本列表显示下拉选择框
        /// </summary>
        private void ShowPopup()
        {
            string[] options = new string[] { "Option 1", "Option 2", "Option 3" };
            int index = options.ToList().IndexOf(holder.test.popupOptionText);
            if (index < 0)
                index = 0;
            int selectedOptionIndex = EditorGUILayout.Popup("Popup:", index, options);
            holder.test.popupOptionText = options[selectedOptionIndex];
        }

        /// <summary>
        /// 基于枚举显示下拉选择框
        /// </summary>
        private void ShowEnumPopup()
        {
            holder.test.popupTestEnum = (TestEnum)EditorGUILayout.EnumPopup("EnumPopup:", holder.test.popupTestEnum);
        }

        /// <summary>
        /// 标签
        /// </summary>
        private void ShowLabelField()
        {
            EditorGUILayout.LabelField("Label:");
        }

        /// <summary>
        /// 输入框
        /// </summary>
        private void ShowTextField()
        {
            holder.test.inputText = EditorGUILayout.TextField("Input:", holder.test.inputText);
        }

        /// <summary>
        /// 输入框
        /// </summary>
        private void ShowIntField()
        {
            holder.test.inputInt = EditorGUILayout.IntField(new GUIContent("InputInt:"), holder.test.inputInt);
        }

        /// <summary>
        /// 输入框
        /// </summary>
        private void ShowFloatField()
        {
            holder.test.inputFloat = EditorGUILayout.FloatField(new GUIContent("InputFloat:"), holder.test.inputFloat);
        }

        /// <summary>
        /// 开关
        /// </summary>
        private void ShowToggle()
        {
            holder.test.isToggle = EditorGUILayout.Toggle(new GUIContent("Toggle:"), holder.test.isToggle);
        }

        /// <summary>
        /// 滑块
        /// </summary>
        private void ShowSlider()
        {
            holder.test.sliderValue = EditorGUILayout.Slider(new GUIContent("Toggle:"), holder.test.sliderValue, 0, 100);
        }

        /// <summary>
        /// 颜色
        /// </summary>
        private void ShowColorField()
        {
            holder.test.color = EditorGUILayout.ColorField(new GUIContent("Color:"), holder.test.color);
        }

        /// <summary>
        /// 多行文本
        /// </summary>
        private void ShowTextArea()
        {
            holder.test.areaText = EditorGUILayout.TextArea(holder.test.areaText);
        }

        /// <summary>
        /// 曲线
        /// </summary>
        private void ShowCurveField()
        {
            holder.test.curve = EditorGUILayout.CurveField("CurveField", holder.test.curve);
        }

        /// <summary>
        /// 曲线
        /// </summary>
        private void ShowGradientField()
        {
            holder.test.gradient = EditorGUILayout.GradientField("GradientField", holder.test.gradient);
        }

        /// <summary>
        /// 二维向量
        /// </summary>
        private void ShowVector2Field()
        {
            holder.test.inputVector2 = EditorGUILayout.Vector2Field("Vector2Field", holder.test.inputVector2);
        }

        /// <summary>
        /// 边界
        /// </summary>
        private void ShowBoundsField()
        {
            holder.test.boundsValue = EditorGUILayout.BoundsField("Bounds Field", holder.test.boundsValue);
        }

        /// <summary>
        /// 说明文字
        /// </summary>
        private void ShowHelpBox()
        {
            EditorGUILayout.HelpBox("1234", MessageType.Info);
        }

        /// <summary>
        /// layerMask
        /// </summary>
        private void ShowLayerField()
        {
            holder.test.layerMask = EditorGUILayout.LayerField("Layer Field", holder.test.layerMask);
        }

        /// <summary>
        /// Tag
        /// </summary>
        private void ShowTagField()
        {
            holder.test.tagValue = EditorGUILayout.TagField("Tag Field", holder.test.tagValue);
        }

        /// <summary>
        /// ObjectField
        /// </summary>
        private void ShowObjectField()
        {
            holder.test.texture = EditorGUILayout.ObjectField("Texture Field", holder.test.texture, typeof(Texture), false) as Texture;
            holder.test.sprite = EditorGUILayout.ObjectField("Sprite Field", holder.test.sprite, typeof(Sprite), false) as Sprite;
        }

        /// <summary>
        /// 文件夹效果
        /// </summary>
        bool foldout1;
        bool foldout2;
        private void ShowFoldout()
        {
            //方法1
            foldout1 = EditorGUILayout.Foldout(foldout1, "Foldout1");
            if (foldout1)
            {
                // 内容在这里
                EditorGUILayout.LabelField("11111");
                EditorGUILayout.LabelField("22222");
            }

            //方法2
            foldout2 = EditorGUILayout.BeginFoldoutHeaderGroup(foldout2, "Foldout2");
            if (foldout2)
            {
                EditorGUILayout.LabelField("33333");
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        /// <summary>
        /// 显示到一行中
        /// </summary>
        private void GUIHorizontal()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Horizontal1");
            EditorGUILayout.LabelField("Horizontal2");
            EditorGUILayout.LabelField("Horizontal3");
            EditorGUILayout.EndHorizontal();
        }

        /// <summary>
        /// PropertyField
        /// </summary>
        private void ShowPropertyField()
        {
            var testProperty = serializedObject.FindProperty("test");
            var valueProperty = testProperty.FindPropertyRelative("serializedPropertyValue");
            EditorGUILayout.PropertyField(valueProperty, new GUIContent("Serialized"), true);
        }
    }
}