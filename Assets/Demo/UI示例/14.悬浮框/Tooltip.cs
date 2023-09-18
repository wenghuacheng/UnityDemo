using DG.Tweening;
using DG.Tweening.Core;
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

                //延迟显示时间
                float delay = i * 0.2f;
                float offest = -1f;

                //从左向右的效果
                StartCoroutine(LeftToRightMoveText(text.transform, offest, delay));

                //淡出效果
                StartCoroutine(FadeText(text, delay));
            }
        }

        /// <summary>
        /// 从左到右进入效果
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator LeftToRightMoveText(Transform transform, float offest, float delay)
        {
            //设置初始位置【这里不是世界坐标】
            var p = transform.localPosition;
            transform.localPosition = new Vector3(offest, p.y, p.z);

            yield return new WaitForSeconds(delay);
            transform.DOLocalMoveX(0, 0.3f);
        }

        /// <summary>
        /// 淡出效果
        /// </summary>
        private IEnumerator FadeText(TextMeshPro textMesh, float delay)
        {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0);
            yield return new WaitForSeconds(delay);
            textMesh.DOFade(1, 1f);
        }
    }
}
