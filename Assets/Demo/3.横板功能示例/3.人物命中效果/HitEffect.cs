using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Demo
{
    /// <summary>
    /// ����Ч��
    /// </summary>
    public class HitEffect : MonoBehaviour
    {
        [SerializeField] private Material hitMaterial;
        [SerializeField] private SpriteRenderer sprite;

        private Material originMaterial;

        private void Awake()
        {
            originMaterial = sprite.material;

            // Э�̲���д��Update�У����Ҷ��Э��ʱ����ֻ��һ����ִ��
            StartCoroutine(Flash());
        }

        private void Update()
        {
            ////����ȷд��
            //if (Input.GetMouseButtonDown(0))
            //{
            //    StopAllCoroutines();
            //    StartCoroutine(Flash());
            //}
        }


        /// <summary>
        /// ͨ���л�����ʵ��Ч��
        /// </summary>
        /// <returns></returns>
        private IEnumerator Flash()
        {
            while (true)
            {
                yield return new LeftMouseButtonDownYieldInstruction();
                Debug.Log("��������Ч��ִ��");
                for (int i = 0; i < 2; i++)
                {
                    sprite.material = hitMaterial;
                    yield return new WaitForSeconds(0.3f);
                    sprite.material = originMaterial;
                    yield return new WaitForSeconds(0.3f);
                }
                Debug.Log("Ч������");
            }
        }
    }

    //�Զ�����ͣ����
    public class LeftMouseButtonDownYieldInstruction : CustomYieldInstruction
    {
        public override bool keepWaiting => !Input.GetMouseButtonDown(0);
    }
}

