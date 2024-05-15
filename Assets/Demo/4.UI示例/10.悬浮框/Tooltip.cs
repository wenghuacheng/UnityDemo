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
         ��Ҫ��ײ����boxCollider�Żᴥ��enter��exit�¼�
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
        /// ���ı����������ƶ�
        /// </summary>
        private void RefreshText()
        {
            for (int i = 0; i < texts.Count; i++)
            {
                var text = texts[i];

                //�ӳ���ʾʱ��
                float delay = i * 0.2f;
                float offest = -1f;

                //�������ҵ�Ч��
                StartCoroutine(LeftToRightMoveText(text.transform, offest, delay));

                //����Ч��
                StartCoroutine(FadeText(text, delay));
            }
        }

        /// <summary>
        /// �����ҽ���Ч��
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator LeftToRightMoveText(Transform transform, float offest, float delay)
        {
            //���ó�ʼλ�á����ﲻ���������꡿
            var p = transform.localPosition;
            transform.localPosition = new Vector3(offest, p.y, p.z);

            yield return new WaitForSeconds(delay);
            transform.DOLocalMoveX(0, 0.3f);
        }

        /// <summary>
        /// ����Ч��
        /// </summary>
        private IEnumerator FadeText(TextMeshPro textMesh, float delay)
        {
            textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0);
            yield return new WaitForSeconds(delay);
            textMesh.DOFade(1, 1f);
        }
    }
}
