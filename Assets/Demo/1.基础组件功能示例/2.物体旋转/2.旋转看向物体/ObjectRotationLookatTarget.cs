using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationLookatTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
    void Start()
    {

    }

    void Update()
    {
        LookTarget01();
        //LookTarget02();
    }

    /// <summary>
    /// 方法1
    /// </summary>
    private void LookTarget01()
    {
        //方向向量
        var vectorToTarget = (target.transform.position - this.transform.position).normalized;    
        //参数一可以理解为物体正视的方向【类比为人的眼睛，这样看向正前方】
        //参数二可以理解为所看物体的方向【让自身通过旋转身体眼睛始终看向对方】
        this.transform.rotation = Quaternion.LookRotation(Vector3.forward, vectorToTarget);
    }

    /// <summary>
    /// 方法2
    /// </summary>
    private void LookTarget02()
    {
        var vectorToTarget = target.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, vectorToTarget);
    }
}
