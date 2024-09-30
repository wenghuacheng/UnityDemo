using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HB.Operation.State
{
    public class EntityVisual : MonoBehaviour
    {
        [SerializeField] private Material damageMaterial;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float flashTime = 0.2f;

        private Material originMaterial;

        private void Awake()
        {
            originMaterial = spriteRenderer.material;

        }

        /// <summary>
        /// ͨ���任������˸
        /// </summary>
        /// <returns></returns>
        public IEnumerator Flash()
        {
            spriteRenderer.material = damageMaterial;
            yield return new WaitForSeconds(flashTime);
            spriteRenderer.material = originMaterial;
        }

        //�첽ˢ�²���
        public void FlashAsync()
        {
            StartCoroutine(Flash());
        }


    }
}