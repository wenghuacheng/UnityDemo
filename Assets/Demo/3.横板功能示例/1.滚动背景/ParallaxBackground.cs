using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Demo.HB.Backgrounds
{
    public class ParallaxBackground : MonoBehaviour
    {
        private Transform cameraTransform;
        //上一次相机位置
        private Vector3 lastCameraPosition;

        private float textureUnitSize;

        private void Awake()
        {
            cameraTransform = Camera.main.transform;
            lastCameraPosition = cameraTransform.position;

            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            Texture2D texture = sprite.texture;
            Debug.Log($"width:{texture.width},pixelsPerUnit:{sprite.pixelsPerUnit}");
            //通过纹理的宽度/每单位像素，来计算纹理单元大小
            textureUnitSize = texture.width / sprite.pixelsPerUnit;
        }

        private void LateUpdate()
        {
            //获取相机的移动间隔 
            Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

            //移动距离差值
            float parallaxEffectMultiplier = 0.5f;

            //让当前物体跟随相机移动
            //也就是与相机有一个移动距离的差距，
            //不同的差值可以让背景显示远近效果
            //近距离的花草可以设置为1让其与相机移动一致
            //远处的高山可以设置的小一点让其慢一点移动/移动幅度小一些
            this.transform.position += deltaMovement * parallaxEffectMultiplier;
            lastCameraPosition = cameraTransform.position;

            //超过显示单元则同步背景与相机的位置
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSize)
            {
                float offsetPosition = (cameraTransform.position.x - transform.position.x) % textureUnitSize;

                transform.position = new Vector3(cameraTransform.position.x + offsetPosition, transform.position.y);
            }

        }
    }
}