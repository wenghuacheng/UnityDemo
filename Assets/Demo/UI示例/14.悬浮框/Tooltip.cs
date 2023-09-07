using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.UI
{
    public class Tooltip : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject tooltip;

        private void Awake()
        {
            tooltip.SetActive(false);
        }

        /**
         需要碰撞器如boxCollider才会触发enter和exit事件
         */
        private void OnMouseEnter()
        {
            tooltip.SetActive(true);
        }

        private void OnMouseExit()
        {
            tooltip.SetActive(false);
        }
    }
}
