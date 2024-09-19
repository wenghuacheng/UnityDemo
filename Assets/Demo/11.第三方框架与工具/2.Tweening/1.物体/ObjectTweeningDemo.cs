using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTweeningDemo : MonoBehaviour
{
    [SerializeField] private GameObject DoMoveObj;
    [SerializeField] private GameObject DoMoveXObj;
    [SerializeField] private GameObject DoLocalMoveObj;

    [SerializeField] private GameObject DOPunchObj;
    [SerializeField] private GameObject DORotateObj;
    [SerializeField] private GameObject DOScaleObj;
    [SerializeField] private GameObject DOShake;


    void Start()
    {
        //物体移动
        DoMoveObj.transform.DOMove(new Vector3(3, DoMoveObj.transform.position.y, 0), 6);
        DoMoveXObj.transform.DOMoveX(3, 6);
        DoLocalMoveObj.transform.DOLocalMove(new Vector3(3, DoLocalMoveObj.transform.localPosition.y, 0), 6); //Unity中有自身坐标（相对）以及世界坐标（绝对）

        //往返移动
        DOPunchObj.transform.DOPunchPosition(new Vector3(3, 0, 0), 6f, 3, 0.1f); //相对坐标 时间 震动 弹性

        //旋转
        DORotateObj.transform.DORotate(new Vector3(0, 270, 0), 6f);//目标旋转坐标

        //缩放
        DOScaleObj.transform.DOScale(new Vector3(2, 2, 2), 2f);

        //震动的时长，强度，频率，随机的角度
        DOShake.transform.DOShakePosition(6f, 0.5f, 10, 90);
    }

}
