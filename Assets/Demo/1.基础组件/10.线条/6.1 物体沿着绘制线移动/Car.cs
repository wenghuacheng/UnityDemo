using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.LineDemo
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private CarDrawLine drawLine;

        private bool startMovement = false;
        private int moveIndex = 0;
        private Vector3[] positions;

        //需要collider组件
        private void OnMouseDown()
        {
            Debug.Log("OnMouseDown");
            drawLine.StartLine(transform.position);
        }

        private void OnMouseDrag()
        {
            Debug.Log("OnMouseDrag");
            drawLine.UpdateLine();
        }

        private void OnMouseUp()
        {
            startMovement = true;
            positions = new Vector3[drawLine.line.positionCount];
            drawLine.line.GetPositions(positions);
            moveIndex = 0;
        }

        private void Update()
        {
            if (startMovement)
            {
                float speed = 5;
                var curPos = positions[moveIndex];
                transform.position = Vector2.MoveTowards(transform.position, curPos, Time.deltaTime * speed);

                //朝向
                var dir = curPos - transform.position;
                float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x);
                //todo：要研究一下
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90f), speed);

                if (Vector2.Distance(transform.position, curPos) <= 0.05f)
                {
                    moveIndex++;
                }

                if (moveIndex > positions.Length - 1)
                    startMovement = false;
            }
        }
    }
}