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
        //�����ƶ�
        DoMoveObj.transform.DOMove(new Vector3(3, DoMoveObj.transform.position.y, 0), 6);
        DoMoveXObj.transform.DOMoveX(3, 6);
        DoLocalMoveObj.transform.DOLocalMove(new Vector3(3, DoLocalMoveObj.transform.localPosition.y, 0), 6); //Unity�����������꣨��ԣ��Լ��������꣨���ԣ�

        //�����ƶ�
        DOPunchObj.transform.DOPunchPosition(new Vector3(3, 0, 0), 6f, 3, 0.1f); //������� ʱ�� �� ����

        //��ת
        DORotateObj.transform.DORotate(new Vector3(0, 270, 0), 6f);//Ŀ����ת����

        //����
        DOScaleObj.transform.DOScale(new Vector3(2, 2, 2), 2f);

        //�𶯵�ʱ����ǿ�ȣ�Ƶ�ʣ�����ĽǶ�
        DOShake.transform.DOShakePosition(6f, 0.5f, 10, 90);
    }

}
