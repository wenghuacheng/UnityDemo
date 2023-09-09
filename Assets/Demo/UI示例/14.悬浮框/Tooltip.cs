using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Demo.UI
{
    public class Tooltip : MonoBehaviour
    {
        [SerializeField] private GameObject tooltip;
        [SerializeField] private List<TextMeshPro> texts;

        private void Awake()
        {
            tooltip.SetActive(false);

            for (int i = 0; i < texts.Count; i++)
            {
                texts[i].text = $"AAA{i}:BBB{i}";
            }
        }

        /**
         需要碰撞器如boxCollider才会触发enter和exit事件
         */
        private void OnMouseEnter()
        {
            tooltip.SetActive(true);
            RefreshText();
        }

        private void OnMouseExit()
        {
            tooltip.SetActive(false);
        }

        /// <summary>
        /// 让文本从左向右移动
        /// </summary>
        private void RefreshText()
        {
            for (int i = 0; i < texts.Count; i++)
            {
                var text = texts[i];
                var rect = text.GetComponent<RectTransform>();

                //从左向右的效果
                StartCoroutine(LeftToRightMoveText(rect, -30, i * 0.2f));
            }
        }

        /// <summary>
        /// 从左到右进入
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator LeftToRightMoveText(RectTransform transform, int offest, float delay)
        {
            var p = transform.position;
            transform.position = new Vector3(offest, p.y, p.z);

            yield return new WaitForSeconds(delay);
            transform.DOLocalMoveX(0, 0.3f);
        }

        private IEnumerator FadeText(RectTransform transform, int offest, float delay)
        {

        }
    }
}
