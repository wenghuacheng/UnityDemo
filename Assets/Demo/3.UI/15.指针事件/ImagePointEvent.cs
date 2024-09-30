using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.UI.PointEventDemo
{
    /// <summary>
    /// 只针对UI元素，sprite等无效
    /// </summary>
    public class ImagePointEvent : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        #region IPointerEnterHandler
        /// <summary>
        /// 指针进入事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("==OnPointerEnter==");
        }
        #endregion

        #region IPointerExitHandler
        /// <summary>
        /// 指针离开事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("==OnPointerExit==");
        }
        #endregion

        #region IPointerDownHandler
        /// <summary>
        /// 指针按下事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("==OnPointerDown==");
        }
        #endregion

        #region IPointerUpHandler
        /// <summary>
        /// 指针抬起事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("==OnPointerUp==");
        }
        #endregion

        #region IPointerClickHandler
        /// <summary>
        /// 指针单击事件，指针按下+抬起=单击
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("==OnPointerClick==");
        }
        #endregion

    }
}