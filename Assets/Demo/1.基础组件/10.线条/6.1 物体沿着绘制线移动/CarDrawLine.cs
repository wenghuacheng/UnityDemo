using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.LineDemo
{
    public class CarDrawLine : MonoBehaviour
    {
        public LineRenderer line;
        private float minDistance = 0.1f;
        private Vector3 prevPos;


        void Start()
        {
            line = GetComponent<LineRenderer>();
            line.positionCount = 1;

        }

        public void StartLine(Vector2 position)
        {
            line.positionCount = 1;
            line.SetPosition(0, position);
        }

        // Update is called once per frame
        public void UpdateLine()
        {
            //if (Input.GetMouseButton(0))
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