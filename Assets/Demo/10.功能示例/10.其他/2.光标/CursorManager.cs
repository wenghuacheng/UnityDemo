using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Other.CursorDemo
{
    public class CursorManager : MonoBehaviour
    {
        //׼�Ƕ����б�
        [SerializeField] private List<CursorAnimation> cursorAnimationList;

        //��֡������ͼ�л�
        private int frameCount = 1;
        //��ǰ���е��ĸ������ͼ
        private int currentFrame;
        //֡ʱ��
        private float frameTime;

        private CursorAnimation cursorAnimation;

        void Start()
        {
            SetCurrentCursorAnimation(cursorAnimationList[0]);
        }

        void Update()
        {
            frameTime -= Time.deltaTime;
            if (frameTime <= 0)
            {
                //������֡ʱ��ʱ����ת����һ��ͼƬ           
                frameTime += cursorAnimation.frameRate;
                currentFrame = (currentFrame + 1) % frameCount;
                //����frameCount����ʾ��һ��Texture2D
                Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offest, CursorMode.Auto);
            }

            if (Input.GetKeyDown(KeyCode.T)) SetCurrentCursorAnimation(cursorAnimationList[0]);
            if (Input.GetKeyDown(KeyCode.Y)) SetCurrentCursorAnimation(cursorAnimationList[1]);
        }

        /// <summary>
        /// ���õ�ǰ�Ĺ��
        /// </summary>
        /// <param name="cursorAnimation"></param>
        private void SetCurrentCursorAnimation(CursorAnimation cursorAnimation)
        {
            this.cursorAnimation = cursorAnimation;
            currentFrame = 0;
            frameTime = 0;
            //frameTime = cursorAnimation.frameRate;
            frameCount = cursorAnimation.textureArray.Length;
        }

        /// <summary>
        /// ��궯��
        /// ��Ҫ�ڱ༭����������ӿ����л���ǩ
        /// </summary>
        [Serializable]
        public class CursorAnimation
        {
            //�����ͼ
            public Texture2D[] textureArray;
            //֡��ʱ��
            public float frameRate;
            public Vector2 offest;
        }
    }
}