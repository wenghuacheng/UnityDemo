using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.UI
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("���յ��϶���Ʒ");

            if (eventData.pointerDrag != null)
            {
                //��ȡ�϶��Ķ���
                var rect = eventData.pointerDrag.GetComponent<RectTransform>();
                //���϶���������ĵ��뱻�϶�������һ�£��������Ǳ��϶����������
                rect.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }
}