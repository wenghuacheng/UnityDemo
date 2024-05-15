using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.ThreeD
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        private float translateSpeed = 2f;//平移速度
        private float rotationSpeed = 5f;//旋转速度
        private float wheelSpeed = 5f;//滚轮速度

        private Camera _camera;

        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        void Update()
        {
            CameraTranslate();
            CameraHorizontalRotation();
            CameraVerticalRotation();
            CameraEnlargeShrink();
        }

        #region 左右上下平移
        private void CameraTranslate()
        {
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.position += new Vector3(-translateSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.transform.position += new Vector3(translateSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                this.transform.position += new Vector3(0, translateSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.transform.position += new Vector3(0, -translateSpeed * Time.deltaTime);
            }
        }
        #endregion

        #region 旋转
        private void CameraHorizontalRotation()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                this.transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            }
            else if (Input.GetKey(KeyCode.E))
            {
                this.transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
            }
        }
        #endregion

        #region 俯仰
        private void CameraVerticalRotation()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                this.transform.Rotate(new Vector3(-rotationSpeed * Time.deltaTime,0 , 0));
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                this.transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0, 0));
            }
        }
        #endregion

        #region 放大缩小
        private void CameraEnlargeShrink()
        {
            var input = Input.GetAxis("Mouse ScrollWheel") * wheelSpeed;//滚轮
            _camera.fieldOfView -= input;
        }

        #endregion
    }
}