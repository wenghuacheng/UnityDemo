using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTowerTransformUpRotationDemo : MonoBehaviour
{
    [SerializeField]private float speed = 2;

    private bool IsRotating;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        var position = Input.mousePosition;
        var mousePos = mainCamera.ScreenToWorldPoint(position);

        if (!IsRotating)
        {            
            //StartCoroutine(RotateByUpRotateTowards(target.transform.position));
            StartCoroutine(RotateByUpVector(mousePos));
        }
    }

    /// <summary>
    /// 通过修改up向量使其旋转
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private IEnumerator RotateByUpVector(Vector3 target)
    {
        IsRotating = true;

        Vector2 direction = (target - transform.position).normalized;

        while (Vector2.Angle(transform.up, direction) >= 0.5f)
        {
            //直接修改UP向量，让其旋转至指向
            transform.up = Vector3.RotateTowards(transform.up, direction.normalized, Time.deltaTime, 0);
            ////MoveTowards与RotateTowards效果相同，本质就是把up向量转变为指向的方向向量
            //transform.up = Vector3.MoveTowards(transform.up, direction.normalized, Time.deltaTime);
            yield return null;
        }

        IsRotating = false;
    }



    /// <summary>
    /// 通过RotateTowards旋转
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    [Obsolete("四元数在2D中比较麻烦，直接用下面的方式")]
    IEnumerator RotateByUpRotateTowards(Vector3 target)
    {
        IsRotating = true;

        Vector3 direction = (target - transform.position).normalized;

        //通过方向获取四元数
        Quaternion targetRotation = Quaternion.LookRotation(this.transform.up, direction);

        double angle = Quaternion.Angle(transform.rotation, targetRotation);
        while (angle > 1f)
        {
            /**
             * 【四元数旋转存在问题，因为会导致Z轴变化，这个后面找原因】
             */

            //一点点旋转向目标角度
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);

            angle = Quaternion.Angle(transform.rotation, targetRotation);
            yield return null;
        }

        IsRotating = false;
    }


}
