using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCameraAfterTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Start()
    {

    }

    void Update()
    {
       
    }

    private void LateUpdate()
    {
        //在LateUpdate中移动可以解决抖动问题

        // CameraSmoothDampMove01();
        CameraSmoothDampMove02();
    }

    /// <summary>
    /// 缓动跟随物体
    /// </summary>
    private void CameraSmoothDampMove01()
    {
        Vector3 velocity = Vector3.zero;
        this.transform.position = Vector3.SmoothDamp(this.transform.position
            , new Vector3(target.position.x, target.position.y, this.transform.position.z), ref velocity, 0.06f);
    }

    /// <summary>
    /// 当超过一定的移动范围，相机才跟随
    /// </summary>
    private void CameraSmoothDampMove02()
    {
        Vector3 velocity = Vector3.zero;

        var distance = Vector2.Distance(this.transform.position, target.position);

        //超过距离才移动
        if(distance>1f)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position
        , new Vector3(target.position.x, target.position.y, this.transform.position.z), ref velocity, 0.06f);
        }
    }
}
