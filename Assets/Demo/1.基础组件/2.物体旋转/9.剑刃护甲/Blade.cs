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

            //LookAtTarget(target);//新增剑刃时会导致不会看向目标
            //AutoRotateAroundTarget(target);
        }

        /// <summary>
        /// 看向物体
        /// </summary>
        /// <param name="target"></param>
        public void LookAtTarget(Transform target)
        {
            var vectorToTarget = this.transform.position - target.transform.position;
            var rotateVectorToTarget = Quaternion.Euler(0, 0, 0) * vectorToTarget;
            this.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);
        }

        /// <summary>
        /// 绕着物体转
        /// </summary>
        public void AutoRotateAroundTarget(Transform target, float bladeRotationSpeed)
        {
            //注意：这里让物体绕着z轴旋转
            this.transform.RotateAround(target.position, new Vector3(0, 0, 1), bladeRotationSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //自己的剑刃触碰不能销毁
            if (collision.transform.parent == this.transform.parent)
                return;

            OnBladeDestory?.Invoke(this);
            Destroy(this.gameObject);
        }
    }
}
