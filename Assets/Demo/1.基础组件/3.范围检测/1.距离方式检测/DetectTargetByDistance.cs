using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectDetection
{
    public class DetectTargetByDistance : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private SpriteRenderer _renderer;
        private const float Range = 2;

        private void Start()
        {
            _renderer = target.transform.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (Vector2.Distance(this.transform.position, target.transform.position) < Range)
            {
                _renderer.color = Color.red;
            }
            else
            {
                _renderer.color = Color.white;
            }
        }

        //»æÖÆ·¶Î§
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, Range);
        }
    }
}