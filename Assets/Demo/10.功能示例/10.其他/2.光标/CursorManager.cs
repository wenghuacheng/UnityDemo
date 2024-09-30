using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Other.CursorDemo
{
    public class CursorManager : MonoBehaviour
    {
        //准星动画列表
        [SerializeField] private List<CursorAnimation> cursorAnimationList;

        //几帧数量贴图切换
        private int frameCount = 1;
        //当前运行到哪个光标贴图
        private int currentFrame;
        //帧时间
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
                //当到达帧时间时就跳转到下一张图片           
                frameTime += cursorAnimation.frameRate;
                currentFrame = (currentFrame + 1) % frameCount;
                //当满frameCount则显示下一个Texture2D
                Cursor.SetCursor(cursorAnimation.textureArray[currentFrame], cursorAnimation.offest, CursorMode.Auto);
            }

            if (Input.GetKeyDown(KeyCode.T)) SetCurrentCursorAnimation(cursorAnimationList[0]);
            if (Input.GetKeyDown(KeyCode.Y)) SetCurrentCursorAnimation(cursorAnimationList[1]);
        }

        /// <summary>
        /// 设置当前的光标
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
        /// 光标动画
        /// 需要在编辑器中设置添加可序列化标签
        /// </summary>
        [Serializable]
        public class CursorAnimation
        {
            //光标贴图
            public Texture2D[] textureArray;
            //帧数时间
            public float frameRate;
            public Vector2 offest;
        }
    }
}