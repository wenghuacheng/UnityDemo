using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationRotateAndLookatTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        var rotationSpeed = 30;

        //看向物体【就是前面示例中的】
        var vectorToTarget = target.transform.position - this.transform.position;
        var rotateVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;
        this.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);

        //这里从Vector3.forward改为back
        this.transform.RotateAround(target.position, Vector3.back, rotationSpeed * Time.deltaTime);
    }

}
