using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera
{
    public class Camera3dController : MonoBehaviour
    {
        public float mouseSensitivity = 1;

        private Camera mCamera;

        void Start()
        {
            //好像时锁定Mouse X的但是没效果
            Cursor.lockState = CursorLockMode.None;
        }

        // Update is called once per frame
        void Update()
        {
            //鼠标移动控制相机旋转
            var mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x,
                this.transform.rotation.eulerAngles.y+ mouseInput.x,
                this.transform.rotation.eulerAngles.z);
        }
    }
}