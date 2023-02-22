using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float _speedX = 0;
    private float _speedY = 0;

    [Header("")]
    public float Speed = 40;

    private const string Animator_IsJump = "IsJump";
    private const string Animator_Speed = "Speed";

    private void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_speedX, _speedY);
       
    }
}
