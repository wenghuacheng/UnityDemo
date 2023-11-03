using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Maps
{
    /// <summary>
    /// 精灵的材质透明度控制
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

            //这里的名字不是name而是reference
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