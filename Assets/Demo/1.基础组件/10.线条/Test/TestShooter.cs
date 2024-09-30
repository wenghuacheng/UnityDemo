using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShooter : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        var input = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        input.z = 0;
        this.transform.position = input;
    }
}
