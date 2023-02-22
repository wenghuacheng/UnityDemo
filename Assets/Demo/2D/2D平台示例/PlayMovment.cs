using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMovment : MonoBehaviour
{
    public CharacterController2D characterController;
    public Animator animator;

    private float _speed = 100;
    private float _horizontalMove = 0;

    private bool isJump = false;
    private bool isCrouch = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _speed;
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log($"{isJump},{isCrouch}");
            isJump = true;

        }
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouch = true;
        }
        if (Input.GetButtonUp("Crouch"))
        {
            isCrouch = false;
        }
        animator.SetBool("IsCrouch", isCrouch);
        
    }

    private void FixedUpdate()
    {
        characterController.Move(_horizontalMove * Time.fixedDeltaTime, isCrouch, isJump);
        isJump = false;
    }
}
