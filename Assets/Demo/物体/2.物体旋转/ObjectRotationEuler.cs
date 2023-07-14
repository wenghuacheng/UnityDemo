using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationEuler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var degress = 30;

        ////Euler方式
        //this.transform.rotation = Quaternion.Euler(Vector3.forward * degress);

        //AngleAxis方式
        this.transform.rotation = Quaternion.AngleAxis(degress,Vector3.forward );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
