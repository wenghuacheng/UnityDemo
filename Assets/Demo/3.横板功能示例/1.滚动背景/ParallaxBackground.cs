using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Demo.HB.Backgrounds
{
    public class ParallaxBackground : MonoBehaviour
    {
        private Transform cameraTransform;
        //��һ�����λ��
        private Vector3 lastCameraPosition;

        private float textureUnitSize;

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;

            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            Debug.Log($"width:{texture.width},pixelsPerUnit:{sprite.pixelsPerUnit}");
            //ͨ������Ŀ��/ÿ��λ���أ�����������Ԫ��С
            textureUnitSize = texture.width / sprite.pixelsPerUnit;
        }

        private void LateUpdate()
        {
            //��ȡ������ƶ���� 
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

            //�ƶ������ֵ
            float parallaxEffectMultiplier = 0.5f;

            //�õ�ǰ�����������ƶ�
            //Ҳ�����������һ���ƶ�����Ĳ�࣬
            //��ͬ�Ĳ�ֵ�����ñ�����ʾԶ��Ч��
            //������Ļ��ݿ�������Ϊ1����������ƶ�һ��
            //Զ���ĸ�ɽ�������õ�Сһ��������һ���ƶ�/�ƶ�����СһЩ
            this.transform.position += deltaMovement * parallaxEffectMultiplier;
            lastCameraPosition = cameraTransform.position;

            //������ʾ��Ԫ��ͬ�������������λ��
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSize)
            {
                float offsetPosition = (cameraTransform.position.x - transform.position.x) % textureUnitSize;

                transform.position = new Vector3(cameraTransform.position.x + offsetPosition, transform.position.y);
            }

        }
    }
}