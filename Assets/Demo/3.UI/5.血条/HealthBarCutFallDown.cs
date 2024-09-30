using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.UI
{
    public class HealthBarCutFallDown : MonoBehaviour
    {
        private RectTransform rectTransform;
        private Image image;
        private float fallDownTime;
        private float fadeTime;
        private Color color;

        private float fallSpeed = 50f;
        private float alphaFadeSpeed = 5;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            image = transform.GetComponent<Image>();
            color = image.color;
            fallDownTime = 1f;
            fadeTime = 1f;

        }

        private void Update()
        {
            fallDownTime -= Time.deltaTime;
            if (fallDownTime < 0f)
            {
                rectTransform.anchoredPosition += Vector2.down * fallSpeed * Time.deltaTime;

                fadeTime -= Time.deltaTime;
                if (fadeTime < 0f)
                {
                    color.a -= alphaFadeSpeed * Time.deltaTime;
                    image.color = color;

                    if (color.a < 0f)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}