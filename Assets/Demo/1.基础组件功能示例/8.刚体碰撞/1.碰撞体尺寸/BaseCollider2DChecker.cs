using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.Rb.CollisonDemo
{
    /// <summary>
    /// »ù´¡
    /// </summary>
    public abstract class BaseCollider2DChecker : MonoBehaviour
    {
        private Camera mainCamera;
        [SerializeField] protected LayerMask layerMask;

        protected virtual void Awake()
        {
            mainCamera = Camera.main;
        }

        protected void Update()
        {
            var worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector2(worldPos.x, worldPos.y);

            if (Input.GetMouseButtonDown(0))
            {
                CheckCollider();
            }
        }

        protected abstract void CheckCollider();
    }
}