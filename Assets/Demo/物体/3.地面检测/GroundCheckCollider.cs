using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckCollider : MonoBehaviour
{
    [SerializeField] private bool isGround;

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGround=true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGround = false;
    }
}
