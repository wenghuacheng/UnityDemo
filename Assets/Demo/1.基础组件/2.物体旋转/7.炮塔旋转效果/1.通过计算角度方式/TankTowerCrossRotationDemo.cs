using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ObjectRotation
{
    /// <summary>
    /// ���ڵ�˼����������תЧ��
    /// </summary>
    public class TankTowerCrossRotationDemo : MonoBehaviour
    {
        [SerializeField] private Transform tower;

        private Camera _camera;

        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            //��ȡ�������ڵ����������
            var position = Input.mousePosition;
            var worldPosition = _camera.ScreenToWorldPoint(position);

            //��ȡ��ת����
            var rotateVector = GetRotateVector(tower, worldPosition);
            if (rotateVector == Vector3.zero) return;

            //������ת����������ת
            tower.Rotate(rotateVector * 50 * Time.deltaTime);
        }


        /// <summary>
        /// ��ȡ��ת������
        /// </summary>
        /// <param name="self"></param>
        /// <param name="targetPostion"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static Vector3 GetRotateVector(Transform self, Vector3 targetPostion)
        {
            // ��������ָ�����λ�õ�����
            // Ŀ��λ��-��ǰλ�ò����ƶ�����
            var direction = (targetPostion - self.position).normalized;


            //�����ڹ���Ŀ��λ�õļн�
            //������ĽǶ�����������û�з���
            var angle = Vector2.Angle(direction, self.up);

            if (Mathf.Approximately(angle, 0)) return Vector3.zero;

            //unity����ʱ��Ƕ���������
            var rotateVector = new Vector3(0, 0, 1);

            //ͨ����˼������ת����
            var from = self.up.normalized;
            var to = direction.normalized;
            var cross = Vector3.Cross(from, to);

            //ע�⣺2d��3d�ж�������һ��
            //����ע�������������
            if (cross.z < 0)
            {
                rotateVector = rotateVector * -1;
            }

            return rotateVector;
        }
    }
}