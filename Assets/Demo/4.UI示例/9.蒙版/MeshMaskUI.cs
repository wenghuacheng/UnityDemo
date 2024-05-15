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
        //��¼���ֿ�ʼ�仯���ȥ���¼�
        private float time = 0f;

        private Vector2 anchoredPosition = Vector2.zero;
        private float width = 0f;
        private float height = 0f;

        protected override void Awake()
        {
            //��ȡ���ֵĿ��
            width = this.rectTransform.rect.width;
            height = this.rectTransform.rect.height;
            anchoredPosition = this.rectTransform.anchoredPosition;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            //���Ͻǵ㡾�����ı�����������Ҫ/2��
            var leftTopPostion = new Vector2(anchoredPosition.x - width / 2, anchoredPosition.y / 2);

            //��ȡ����X��λ��
            float maxX = Math.Min(leftTopPostion.x + time * 100, leftTopPostion.x + width);

            //ע�����������Ļ���˳����ʱ��/˳ʱ�룬�������ֲ�����ͼ�Ρ�
            Vector3 vec1 = new Vector3(leftTopPostion.x, anchoredPosition.y - height / 2);//�������Ͻǵ�
            Vector3 vec2 = new Vector3(leftTopPostion.x, anchoredPosition.y + height / 2);//�������½ǵ�
                                                                                          //�����Ҳ���������X��Ҫ���б仯�������ֱ��ʹ����ʾ���������
            Vector3 vec3 = new Vector3(maxX, anchoredPosition.y + height / 2);//�������½ǵ�
            Vector3 vec4 = new Vector3(maxX, anchoredPosition.y - height / 2);//�������Ͻǵ�

            //��������������Ҫ��alphaֵ
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
                //�������ػ�
                SetVerticesDirty();
            }
        }
    }
}