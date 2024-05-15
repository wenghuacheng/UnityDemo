using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.UI.PointEventDemo
{
    public class PointerOverChecker : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("点击在UI元素上");
                }
                else
                {
                    Debug.Log("点击在不在UI元素上");
                }
            }
        }
    }
}