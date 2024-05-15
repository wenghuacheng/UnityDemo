using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.ThreeD
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        private float translateSpeed = 2f;//ƽ���ٶ�
        private float rotationSpeed = 5f;//��ת�ٶ�
        private float wheelSpeed = 5f;//�����ٶ�

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

        #region ��������ƽ��
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

        #region ��ת
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

        #region ����
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

        #region �Ŵ���С
        private void CameraEnlargeShrink()
        {
            var input = Input.GetAxis("Mouse ScrollWheel") * wheelSpeed;//����
            _camera.fieldOfView -= input;
        }

        #endregion
    }
}