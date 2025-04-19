using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

namespace Demo.UI
{
    public class ImageSelectionHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //设置后才会触发OnSelect
            eventData.selectedObject = this.gameObject;
            //Debug.Log("Enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //设置后才会触发OnDeselect
            eventData.selectedObject = null;
            //Debug.Log("Exit");
        }

        public void OnSelect(BaseEventData eventData)
        {
            //Debug.Log("OnSelect");
            image.color = Color.red;
            //注意，默认的color tint会失效，需要代码控制

            ImageSelectionManager.instance.LastSelected = this.gameObject;
            for (int i = 0; ImageSelectionManager.instance.Cards.Length > 0; i++)
            {
                if (ImageSelectionManager.instance.Cards[i] == this.gameObject)
                {
                    ImageSelectionManager.instance.LastSelectedIndex = i;
                    break;
                }
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            //Debug.Log("OnDeselect");
            image.color = Color.white;
            //注意，默认的color tint会失效，需要代码控制
        }

        public void HandleClick()
        {
            Debug.Log("我被点击了");
        }

    }
}