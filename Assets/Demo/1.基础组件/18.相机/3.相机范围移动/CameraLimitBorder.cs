using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera
{
    public class CameraLimitBorder : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D areaBox;

        private Camera mainCamera;

        private float halfWidth;
        private float halfHeight;

        private void Start()
        {
            mainCamera = Camera.main;

            halfHeight = mainCamera.orthographicSize;
            halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        }

        void Update()
        {
            //����������ķ�Χ��Ȼ���������ƶ�
            this.transform.position = new Vector3(
              Mathf.Clamp(this.transform.position.x, areaBox.bounds.min.x + halfWidth, areaBox.bounds.max.x - halfWidth),
               Mathf.Clamp(this.transform.position.y, areaBox.bounds.min.y + halfHeight, areaBox.bounds.max.y - halfHeight),
               this.transform.position.z
              );
        }
    }
}