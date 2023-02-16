using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovment : MonoBehaviour
{
    public CharacterController2D characterController;

    private float _speed = 40;
    private float _horizontalMove = 0;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _speed;
        Debug.Log(_horizontalMove);

    }

    private void FixedUpdate()
    {
        characterController.Move(_horizontalMove*Time.fixedDeltaTime, false, false);
    }
}
