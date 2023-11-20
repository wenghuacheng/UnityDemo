using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Demo.Maps
{
    /// <summary>
    /// 精灵的材质透明度控制
    /// </summary>
    public class TilemapRenderMaterialAlphaController : MonoBehaviour
    {
        [SerializeField] private TilemapRenderer render;

        private void Start()
        {
            StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            float alpha = 0f;
            float duration = 10f;//效果持续时间

            //这里的名字不是name而是reference
            render.material.SetFloat("_Alpha", alpha);
            yield return null;

            while (alpha < 1f)
            {
                alpha += Time.deltaTime / duration;
                render.material.SetFloat("_Alpha", alpha);
                yield return null;
            }

            yield return null;
        }
    }
}