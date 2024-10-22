using Demo.Basic.LineDemo;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Misc
{
    public class BallDragController : MonoBehaviour
    {
        public LineRenderer line;
        public Rigidbody2D rb;

        public float dragLimit = 3f;

        public float forceToAdd = 10f;

        private Camera cam;
        private bool isDragging = false;

        public Vector3 MousePosition
        {
            get
            {
                var pos = cam.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                return pos;
            }
        }

        private void Start()
        {
            cam = Camera.main;

            line.positionCount = 2;
            line.SetPosition(0, Vector2.zero);
            line.SetPosition(0, Vector2.zero);
            line.enabled = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isDragging)
            {
                DragStart();
            }
            if (isDragging)
            {
                Drag();
            }
            if (Input.GetMouseButtonUp(0) && isDragging)
            {
                DragEnd();
            }
        }

        private void DragStart()
        {
            line.enabled = true;
            isDragging = true;
            line.SetPosition(0, MousePosition);
        }


        private void Drag()
        {
            var startPos = line.GetPosition(0);
            var curPos = MousePosition;

            var distance = curPos - startPos;
            if (distance.magnitude <= dragLimit)
            {
                line.SetPosition(1, curPos);
            }
            else
            {
                curPos = startPos + distance.normalized * dragLimit;
                line.SetPosition(1, curPos);
            }

        }


        private void DragEnd()
        {
            line.enabled = false;
            isDragging = false;

            var startPos = line.GetPosition(0);
            var curPos = line.GetPosition(1);
            var distance = curPos - startPos;

            var finalForce = distance * forceToAdd;
            rb.AddForce(finalForce, ForceMode2D.Impulse);
        }

    }
}