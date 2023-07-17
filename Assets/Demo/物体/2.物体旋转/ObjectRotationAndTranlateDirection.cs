using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationAndTranlateDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //旋转时物体的坐标系也会相应旋转
        this.transform.Rotate(0, 0, 45 * Time.deltaTime);
        //物体移动时是基于自身坐标系为准进行移动，即使旋转物体也会向着自身的Y轴正方向进行移动
        //所以会出现物体好像绕着一个点旋转
        this.transform.Translate(Vector3.up * Time.deltaTime);
    }
}
