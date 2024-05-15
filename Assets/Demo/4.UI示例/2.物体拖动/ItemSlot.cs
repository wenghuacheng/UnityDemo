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
            Debug.Log("接收到拖动物品");

            if (eventData.pointerDrag != null)
            {
                //获取拖动的对象
                var rect = eventData.pointerDrag.GetComponent<RectTransform>();
                //将拖动对象的中心点与被拖动的物体一致，这样就是被拖动物体的中心
                rect.anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }
}