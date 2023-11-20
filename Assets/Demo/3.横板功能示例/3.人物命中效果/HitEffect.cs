using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo
{
    /// <summary>
    /// 命中效果
    /// </summary>
    public class HitEffect : MonoBehaviour
    {
        [SerializeField] private Material hitMaterial;
        [SerializeField] private SpriteRenderer sprite;

        private Material originMaterial;

        private void Awake()
        {
            originMaterial = sprite.material;

            // 协程不能写在Update中，而且多个协程时有且只有一个会执行
            StartCoroutine(Flash());
        }

        private void Update()
        {
            ////不正确写法
            //if (Input.GetMouseButtonDown(0))
            //{
            //    StopAllCoroutines();
            //    StartCoroutine(Flash());
            //}
        }


        /// <summary>
        /// 通过切换材质实现效果
        /// </summary>
        /// <returns></returns>
        private IEnumerator Flash()
        {
            while (true)
            {
                yield return new LeftMouseButtonDownYieldInstruction();
                Debug.Log("点击左键，效果执行");
                for (int i = 0; i < 2; i++)
                {
                    sprite.material = hitMaterial;
                    yield return new WaitForSeconds(0.3f);
                    sprite.material = originMaterial;
                    yield return new WaitForSeconds(0.3f);
                }
                Debug.Log("效果结束");
            }
        }
    }

    //自定义暂停条件
    public class LeftMouseButtonDownYieldInstruction : CustomYieldInstruction
    {
        public override bool keepWaiting => !Input.GetMouseButtonDown(0);
    }
}

