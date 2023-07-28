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
        //LookTarget01();
        LookTarget02();
    }

    private void LookTarget01()
    {
        //Ã»¿´¶®
        var vectorToTarget = target.transform.position - this.transform.position;
        var rotateVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

        this.transform.rotation = Quaternion.LookRotation(Vector3.forward, rotateVectorToTarget);
    }

    private void LookTarget02()
    {
        var vectorToTarget = target.transform.position - this.transform.position;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.right, vectorToTarget);
    }
}
