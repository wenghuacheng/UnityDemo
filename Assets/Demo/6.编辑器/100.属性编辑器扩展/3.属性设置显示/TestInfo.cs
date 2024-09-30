using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomEditors
{
    [Serializable]
    public class TestEnumInfo
    {
        //文本下拉框
        public string popupOptionText;
        //枚举下拉框
        public TestEnum popupTestEnum;

        //输入文本框
        public string inputText;
        //输入数字文本框
        public int inputInt;
        //输入数字文本框
        public float inputFloat;
        //开关数值
        public bool isToggle;
        //滑块数值
        public float sliderValue;
        //颜色
        public Color color;
        //多行输入框
        public string areaText;
        //曲线
        public AnimationCurve curve;
        //渐变
        public Gradient gradient;

        public Vector2 inputVector2;

        public Bounds boundsValue;

        public int layerMask;

        public string tagValue;

        public Texture texture;

        public Sprite sprite;

        public string serializedPropertyValue;
    }

    [Serializable]
    public enum TestEnum
    {
        A, B, C, D
    }
}