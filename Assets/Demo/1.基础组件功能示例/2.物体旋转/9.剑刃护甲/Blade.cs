using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectRotation.BladeArmor
{
    public class Blade : MonoBehaviour
    {
        public event Action<Blade> OnBladeDestory;

        void Update()
        {
            AutoRotation();
        }

        private void AutoRotation()
        {
            var target = this.transform.parent;

            //LookAtTarget(target);//��������ʱ�ᵼ�²��ῴ��Ŀ��
            //AutoRotateAroundTarget(target);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="target"></param>
        public void LookAtTarget(Transform target)
        {
            var vectorToTarget = this.transform.position - target.transform.position;
            var rotateVectorToTarget = Quaternion.Euler(0, 0, 0) * vectorToTarget;
            this.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);
        }

        /// <summary>
        /// ��������ת
        /// </summary>
        public void AutoRotateAroundTarget(Transform target, float bladeRotationSpeed)
        {
            //ע�⣺��������������z����ת
            this.transform.RotateAround(target.position, new Vector3(0, 0, 1), bladeRotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //�Լ��Ľ��д�����������
            if (collision.transform.parent == this.transform.parent)
                return;

            OnBladeDestory?.Invoke(this);
            Destroy(this.gameObject);
        }
    }
}
