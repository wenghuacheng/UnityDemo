using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationRotateTarget : MonoBehaviour
{
    [SerializeField] private Transform target;
   
    void Update()
    {
        var rotationSpeed = 30;
        this.transform.RotateAround(target.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
