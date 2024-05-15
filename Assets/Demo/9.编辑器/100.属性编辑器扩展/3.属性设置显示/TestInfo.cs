using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomEditors
{
    [Serializable]
    public class TestEnumInfo
    {
        //�ı�������
        public string popupOptionText;
        //ö��������
        public TestEnum popupTestEnum;

        //�����ı���
        public string inputText;
        //���������ı���
        public int inputInt;
        //���������ı���
        public float inputFloat;
        //������ֵ
        public bool isToggle;
        //������ֵ
        public float sliderValue;
        //��ɫ
        public Color color;
        //���������
        public string areaText;
        //����
        public AnimationCurve curve;
        //����
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