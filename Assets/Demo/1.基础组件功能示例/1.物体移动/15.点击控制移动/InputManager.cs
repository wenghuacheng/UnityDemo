using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.ObjectMove.ClickMove
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private List<ObjectController> controllers;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
                foreach (var item in controllers)
                {
                    item.SetTarget(pos);
                }
            }
        }
    }
}