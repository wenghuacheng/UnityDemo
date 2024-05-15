using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera
{
    public class CameraAfterTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private void LateUpdate()
        {
            //��LateUpdate���ƶ����Խ����������
            CameraSmoothDampMove02();
        }

        /// <summary>
        /// ������һ�����ƶ���Χ������Ÿ���
        /// </summary>
        private void CameraSmoothDampMove02()
        {
            Vector3 velocity = Vector3.zero;

            var distance = Vector2.Distance(this.transform.position, target.position);

            //����������ƶ�
            if (distance > 1f)
            {
                this.transform.position = Vector3.SmoothDamp(this.transform.position
                    ,new Vector3(target.position.x, target.position.y, this.transform.position.z), ref velocity, 0.06f);
            }
        }
    }
}