using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.UI.PointEventDemo
{
    /// <summary>
    /// ֻ���UIԪ�أ�sprite����Ч
    /// </summary>
    public class ImagePointEvent : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region IPointerEnterHandler
        /// <summary>
        /// ָ������¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("==OnPointerEnter==");
        }
        #endregion

        #region IPointerExitHandler
        /// <summary>
        /// ָ���뿪�¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("==OnPointerExit==");
        }
        #endregion

        #region IPointerDownHandler
        /// <summary>
        /// ָ�밴���¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("==OnPointerDown==");
        }
        #endregion

        #region IPointerUpHandler
        /// <summary>
        /// ָ��̧���¼�
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("==OnPointerUp==");
        }
        #endregion

        #region IPointerClickHandler
        /// <summary>
        /// ָ�뵥���¼���ָ�밴��+̧��=����
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("==OnPointerClick==");
        }
        #endregion

    }
}