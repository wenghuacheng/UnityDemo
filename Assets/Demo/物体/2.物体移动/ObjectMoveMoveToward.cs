using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveMoveToward : MonoBehaviour
{
    private float speed = 1;
    private Vector3 target = new Vector3(5,0,0);

    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, speed * Time.deltaTime);
    }
}
