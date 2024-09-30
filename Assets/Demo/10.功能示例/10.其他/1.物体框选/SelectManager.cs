using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Misc
{
    public class SelectManager : MonoBehaviour
    {
        public float MinSize = 10f;
        public float MinTime = 0.1f;
        public GameObject[] allCharacters;//待选择物体

        private float selectTimer = 0f;
        public Vector2 selectingStart;
        private RectTransform selectingBoxRect;//选框

        private void Awake()
        {
            selectingBoxRect = GetComponent<RectTransform>();
        }


        private void Update()
        {
            //鼠标按下
            if (Input.GetMouseButtonDown(0))
            {
                selectingStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                selectingBoxRect.anchoredPosition = selectingStart;//设置选框锚点位置
            }

            //鼠标抬起
            if (Input.GetMouseButtonUp(0))
            {
                if (selectTimer <= MinTime)
                {
                    ClickObject();
                }
                //清理选中状态
                selectTimer = 0f;
                selectingBoxRect.sizeDelta = Vector2.zero;
            }

            //鼠标移动
            if (Input.GetMouseButton(0))
            {
                selectTimer += Time.deltaTime;
                if (selectTimer > MinTime)
                {
                    //框选演示：应该在鼠标抬起时判断
                    SelectingObjects();
                }
            }
        }

        private void ClickObject()
        {

        }

        /// <summary>
        /// 选框
        /// 注意anchor方式
        /// </summary>
        private void SelectingObjects()
        {
            //选框尺寸
            Vector2 sizeDelta = Vector2.zero;
            sizeDelta.x = MathF.Abs(selectingStart.x - Input.mousePosition.x);
            sizeDelta.y = MathF.Abs(selectingStart.y - Input.mousePosition.y);

            Rect rect = Rect.zero;
            Vector2 piot = Vector2.zero;//锚点变更

            if (selectingStart.x - Input.mousePosition.x < 0)
            {
                //移动方向为起始点左侧
                rect.x = selectingStart.x;
            }
            else
            {
                //移动方向为起始点右侧
                piot.x = 1;
                rect.x = selectingStart.x - selectingBoxRect.sizeDelta.x;
            }


            if (selectingStart.y - Input.mousePosition.y > 0)
            {
                //移动方向为起始点上方
                piot.y = 1;
                rect.y = selectingStart.y - selectingBoxRect.sizeDelta.y;
            }
            else
            {
                //移动方向为起始点下方
                rect.y = selectingStart.y;
            }

            selectingBoxRect.sizeDelta = sizeDelta;

            if (selectingBoxRect.pivot != piot)
                selectingBoxRect.pivot = piot;


            rect.width = sizeDelta.x;
            rect.height = sizeDelta.y;

            if (sizeDelta.x > MinSize && sizeDelta.y > MinSize)
            {
                //todo：选择对象
                CheckForSelectedCharacters();
            }
        }


        /// <summary>
        /// 检测物体是否被选中
        /// </summary>
        private void CheckForSelectedCharacters()
        {
            foreach (GameObject character in allCharacters)
            {
                var screenPos = Camera.main.WorldToScreenPoint(character.transform.position);
                if (RectTransformUtility.RectangleContainsScreenPoint(selectingBoxRect, screenPos))
                {
                    Debug.Log("选中");
                }
            }
        }
    }
}