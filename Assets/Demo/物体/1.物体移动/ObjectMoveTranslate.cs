using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveTranslate : MonoBehaviour
{
    private float speed = 1;

    //不要在Update中运行，容易导致低帧率上长度不一致
    void FixedUpdate()
    {
        this.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
}
