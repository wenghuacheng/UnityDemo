using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    private float speed = 2f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vector3 = transform.position;
        if (Input.GetKey(KeyCode.W))
            vector3.y += speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            vector3.y -= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            vector3.x -= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            vector3.x += speed * Time.deltaTime;

        this.transform.position = vector3;
    }
}
