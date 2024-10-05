using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.LineDemo
{
    /// <summary>
    /// 基于鼠标移动绘制线
    /// </summary>
    public class MouseDrawLine : MonoBehaviour
    {
        [SerializeField] private LineRenderer line;

        private float minDistance = 0.1f;
        private Vector3 prevPos;


        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {            
            if (Input.GetMouseButton(0))
            {
                var currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentPos.z = 0;

                if (Vector3.Distance(currentPos, prevPos) > minDistance)
                {
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, currentPos);
                    prevPos = currentPos;
                }
            }

        }
    }
}