using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEffect : MonoBehaviour
{
    private Vector2 target;
    //插值
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
            //用当前坐标算出新的旋转向量
            Vector2 newDirection = (target - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;

            //平滑过渡，从一个方位向量向一个方位向量平滑过渡
            //通过一个变量来改变转向速度，即离目标点越近转向速度越快
            this.transform.up = Vector3.Slerp(this.transform.up, newDirection, lerp / Vector2.Distance(target, this.transform.position));

            //到达目标后不进行旋转而是向当前方向继续前进
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
