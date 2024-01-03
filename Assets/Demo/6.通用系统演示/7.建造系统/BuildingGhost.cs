using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Common.Build
{
    /// <summary>
    /// ‘§œ»Ω®‘Ï
    /// </summary>
    public class BuildingGhost : MonoBehaviour
    {
        public event Action OnMouseClick;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseClick?.Invoke();
            }
        }
    }
}