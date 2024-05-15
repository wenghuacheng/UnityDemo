using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Maps
{
    /// <summary>
    /// ����Ĳ���͸���ȿ���
    /// </summary>
    public class SpriteRenderMaterialAlphaController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer render;

        void Start()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            float alpha = 0f;

            //��������ֲ���name����reference
            render.material.SetFloat("_Alpha", alpha);
            yield return null;

            while (alpha < 1f)
            {
                alpha += Time.deltaTime;
                render.material.SetFloat("_Alpha", alpha);
                yield return null;
            }

            yield return null;
        }
    }
}