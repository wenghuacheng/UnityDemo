using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Other.Trails.TrailRenders
{
    public class Player : MonoBehaviour
    {
        private Camera _camera;

        void Start()
        {
            //Cursor.visible = false;
            _camera = Camera.main;
        }

        void Update()
        {
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(pos.x, pos.y);
        }
    }
}