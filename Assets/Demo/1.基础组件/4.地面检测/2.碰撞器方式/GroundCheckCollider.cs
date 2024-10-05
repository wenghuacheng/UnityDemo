using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GroundDetection
{
    public class GroundCheckCollider : MonoBehaviour
    {
        [field: SerializeField] public bool isGround { get; private set; }

        private void OnTriggerStay2D(Collider2D collision)
        {
            isGround = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            isGround = false;
        }
    }
}