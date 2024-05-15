using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectDetection
{
    public class DetectTargetByPhysics : MonoBehaviour
    {
        public const int Range = 2;
        [SerializeField]
        private LayerMask layerMask;
        [SerializeField]
        private Transform target;

        void Update()
        {
            var objs = Physics2D.OverlapCircleAll(this.transform.position, Range, layerMask).Select(p => p.transform).ToList();
            var _renderer = target.transform.GetComponent<SpriteRenderer>();
            if (objs.Contains(target))
            {
                _renderer.color = Color.red;
            }
            else
            {
                _renderer.color = Color.white;
            }
        }
    }
}
