using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectTargetByCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
