using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Demo.Maps
{
    /// <summary>
    /// ����Ĳ���͸���ȿ���
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
            float duration = 10f;//Ч������ʱ��

            //��������ֲ���name����reference
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