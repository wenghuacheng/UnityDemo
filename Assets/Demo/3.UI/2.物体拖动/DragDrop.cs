using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.UI
{
    public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Canvas canvas;
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = this.GetComponent<RectTransform>();
        }

        /// <summary>
        /// ��ʼ��ק
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("��ʼ��ק");
            GetComponent<CanvasGroup>().blocksRaycasts = false;

        }

        /// <summary>
        /// ��ק��
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("��ק��");

            //��Ҫ����canvas������
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        /// <summary>
        /// ������ק
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("������ק");
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
}