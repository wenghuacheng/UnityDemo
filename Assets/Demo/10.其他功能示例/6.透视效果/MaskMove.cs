using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Other.ObjMask
{
    /// <summary>
    /// 通过移动SpriteMask，显示Mask Interaction为inside Mask
    /// </summary>
    public class MaskMove : MonoBehaviour
    {
        private Camera _camera;

        void Start()
        {
            _camera = Camera.main;
        }

        void Update()
        {
            var p = _camera.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(p.x, p.y);
        }
    }
}