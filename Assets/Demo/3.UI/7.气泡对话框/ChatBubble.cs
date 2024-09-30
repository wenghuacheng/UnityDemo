using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo.UI
{
    public class ChatBubble : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer bgSpriteRenderer;
        [SerializeField] private TextMeshPro textMeshPro;

        private void Start()
        {
            SetText("ASDASDASLKDJLKZXVVJLXCIVOPIWEOPFJKLSJDKLGV");
        }

        public void SetText(string text)
        {
            textMeshPro.SetText(text);
            //修改TextMesh属性会立即生效。下面才能获取真实的文本尺寸
            textMeshPro.ForceMeshUpdate();

            //获取更新后的文本尺寸
            var textSize = textMeshPro.GetRenderedValues(false);
            //Vector2 padding = new Vector2(2f, 1f);
            Vector2 padding = new Vector2(1f, 0.5f);
            bgSpriteRenderer.size = textSize + padding;

            bgSpriteRenderer.transform.localPosition = new Vector3(bgSpriteRenderer.size.x / 2f, bgSpriteRenderer.transform.localPosition.y);
        }
    }
}