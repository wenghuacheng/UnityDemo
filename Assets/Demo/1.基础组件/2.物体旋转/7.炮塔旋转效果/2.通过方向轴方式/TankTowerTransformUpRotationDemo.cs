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
    /// ͨ���޸�up����ʹ����ת
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private IEnumerator RotateByUpVector(Vector3 target)
    {
        IsRotating = true;

        Vector2 direction = (target - transform.position).normalized;

        while (Vector2.Angle(transform.up, direction) >= 0.5f)
        {
            //ֱ���޸�UP������������ת��ָ��
            transform.up = Vector3.RotateTowards(transform.up, direction.normalized, Time.deltaTime, 0);
            ////MoveTowards��RotateTowardsЧ����ͬ�����ʾ��ǰ�up����ת��Ϊָ��ķ�������
            //transform.up = Vector3.MoveTowards(transform.up, direction.normalized, Time.deltaTime);
            yield return null;
        }

        IsRotating = false;
    }



    /// <summary>
    /// ͨ��RotateTowards��ת
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    [Obsolete("��Ԫ����2D�бȽ��鷳��ֱ��������ķ�ʽ")]
    IEnumerator RotateByUpRotateTowards(Vector3 target)
    {
        IsRotating = true;

        Vector3 direction = (target - transform.position).normalized;

        //ͨ�������ȡ��Ԫ��
        Quaternion targetRotation = Quaternion.LookRotation(this.transform.up, direction);

        double angle = Quaternion.Angle(transform.rotation, targetRotation);
        while (angle > 1f)
        {
            /**
             * ����Ԫ����ת�������⣬��Ϊ�ᵼ��Z��仯�����������ԭ��
             */

            //һ�����ת��Ŀ��Ƕ�
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime);

            angle = Quaternion.Angle(transform.rotation, targetRotation);
            yield return null;
        }

        IsRotating = false;
    }


}
