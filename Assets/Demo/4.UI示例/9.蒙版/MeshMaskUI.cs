using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class MeshMaskUI : MaskableGraphic
    {
        [SerializeField] private bool IsStart = false;
        //记录遮罩开始变化后过去的事件
        private float time = 0f;

        private Vector2 anchoredPosition = Vector2.zero;
        private float width = 0f;
        private float height = 0f;

        protected override void Awake()
        {
            //获取文字的宽高
            width = this.rectTransform.rect.width;
            height = this.rectTransform.rect.height;
            anchoredPosition = this.rectTransform.anchoredPosition;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            //左上角点【这里文本居中所以需要/2】
            var leftTopPostion = new Vector2(anchoredPosition.x - width / 2, anchoredPosition.y / 2);

            //获取最大的X的位置
            float maxX = Math.Min(leftTopPostion.x + time * 100, leftTopPostion.x + width);

            //注意遮罩区域点的绘制顺序【逆时针/顺时针，否则会出现不规则图形】
            Vector3 vec1 = new Vector3(leftTopPostion.x, anchoredPosition.y - height / 2);//遮罩左上角点
            Vector3 vec2 = new Vector3(leftTopPostion.x, anchoredPosition.y + height / 2);//遮罩左下角点
                                                                                          //遮罩右侧的两个点的X需要进行变化，让遮罩变大使其显示后面的文字
            Vector3 vec3 = new Vector3(maxX, anchoredPosition.y + height / 2);//遮罩右下角点
            Vector3 vec4 = new Vector3(maxX, anchoredPosition.y - height / 2);//遮罩右上角点

            //绘制区域。这样需要有alpha值
            vh.AddUIVertexQuad(new UIVertex[] {
            new UIVertex{ position=vec1,color=new Color32(0,0,0,1) },
            new UIVertex{ position=vec2,color=new Color32(0,0,0,1) },
            new UIVertex{ position=vec3,color=new Color32(0,0,0,1) },
            new UIVertex{ position=vec4,color=new Color32(0,0,0,1) }
        });
        }

        public void Update()
        {
            if (IsStart)
            {
                time += Time.deltaTime;
                //让遮罩重绘
                SetVerticesDirty();
            }
        }
    }
}