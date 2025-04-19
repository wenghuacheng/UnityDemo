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
            //���ú�Żᴥ��OnSelect
            eventData.selectedObject = this.gameObject;
            //Debug.Log("Enter");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //���ú�Żᴥ��OnDeselect
            eventData.selectedObject = null;
            //Debug.Log("Exit");
        }

        public void OnSelect(BaseEventData eventData)
        {
            //Debug.Log("OnSelect");
            image.color = Color.red;
            //ע�⣬Ĭ�ϵ�color tint��ʧЧ����Ҫ�������

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
            //ע�⣬Ĭ�ϵ�color tint��ʧЧ����Ҫ�������
        }

        public void HandleClick()
        {
            Debug.Log("�ұ������");
        }

    }
}