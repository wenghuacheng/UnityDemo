using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CatLike.ObjectManager.Chaper02
{
    public class Shape : MonoBehaviour
    {
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetMaterial(Material material)
        {
            meshRenderer.material = material;
        }

        public void SetColor(Color color)
        {
            meshRenderer.material.color = color;

            //[û��Ч��]
            ////���������ơ���ֹ�����µ�material����ɫ�������ñ�����
            //var colorPropertyId = Shader.PropertyToID("_Color");
            //var propertyBlock = new MaterialPropertyBlock();
            //propertyBlock.SetColor(colorPropertyId, color);
            ////propertyBlock.SetColor("_Color", Color.red);
            //meshRenderer.SetPropertyBlock(propertyBlock,0);
        }
    }
}