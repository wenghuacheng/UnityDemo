using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour,IDragHandler,IEndDragHandler
{
    public RectTransform target;
    private bool isTrigger;

    public void OnDrag(PointerEventData eventData)
    {
        //通过增量移动
        target.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //判断是否碰到了某个物体
        if (isTrigger)
        {
            this.target.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            this.target.GetComponent<Image>().color = Color.gray;
        }
      
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTrigger = true;
        this.target.GetComponent<Image>().color = Color.yellow;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isTrigger = false;
        this.target.GetComponent<Image>().color = Color.white;
    }
}
