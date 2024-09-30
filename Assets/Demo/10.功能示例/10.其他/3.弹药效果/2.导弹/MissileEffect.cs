using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEffect : MonoBehaviour
{
    private Vector2 target;
    //��ֵ
    private float lerp =0.1f;
    private bool isArrived = false;

    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isArrived)
        {
            //�õ�ǰ��������µ���ת����
            Vector2 newDirection = (target - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;

            //ƽ�����ɣ���һ����λ������һ����λ����ƽ������
            //ͨ��һ���������ı�ת���ٶȣ�����Ŀ���Խ��ת���ٶ�Խ��
            this.transform.up = Vector3.Slerp(this.transform.up, newDirection, lerp / Vector2.Distance(target, this.transform.position));

            //����Ŀ��󲻽�����ת������ǰ�������ǰ��
            if (Vector2.Distance(this.transform.position, target) < 0.1f)
            {
                isArrived = true;
            }
        }

        this.transform.Translate(this.transform.up * Time.deltaTime * 3, Space.World);      
    }

    public void SetTarget(Vector2 target)
    {
        this.target = target;
    }
}
