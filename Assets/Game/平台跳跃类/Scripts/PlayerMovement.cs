using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private float _speedX = 0;
    private bool _isJump = false;
    public bool _isGround = false;

    public float Speed = 1;

    private const string Animator_IsJump = "IsJump";
    private const string Animator_Speed = "Speed";

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody2D>();
        _animator = this.GetComponent<Animator>();
        _spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        _speedX = x * Speed;

        if (Input.GetButtonDown("Jump") && _isGround)
        {
            _isJump = true;
        }
    }

    private void FixedUpdate()
    {
        ////速度太快
        //if (_speedX != 0)
        //{
        //    var direction = _speedX > 0 ? Vector2.right : Vector2.left;
        //    _rigidbody.AddForce(direction * Speed, ForceMode2D.Impulse);
        //    _animator.SetFloat(Animator_Speed, math.abs(_speedX));
        //}d
        //else
        //{
        //    _animator.SetFloat(Animator_Speed, 0);
        //}

        //判断是否触底
        CheckGround();

        //水平移动
        _rigidbody.velocity = new Vector2(_speedX, 0) * Time.fixedDeltaTime;
        _animator.SetFloat(Animator_Speed, math.abs(_speedX));

        //跳跃
        if (_isJump)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 150);
            _animator.SetBool(Animator_IsJump, true);
            _isJump = false;
        }

        //设置人物是否反转
        FlipPlayer();

    }

    /// <summary>
    /// 人物反转
    /// </summary>
    private void FlipPlayer()
    {
        if (_spriteRenderer == null) return;

        //人物反转
        if (_speedX < 0 && !_spriteRenderer.flipX)
            _spriteRenderer.flipX = true;
        else if (_speedX > 0 && _spriteRenderer.flipX)
            _spriteRenderer.flipX = false;
    }

    /// <summary>
    /// 判断是否触底
    /// </summary>
    private void CheckGround()
    {
        var collison = Physics2D.Raycast(this.transform.position, Vector3.down, 0.4f, LayerMask.GetMask("Ground"));
        if (collison.collider != null)
        {
            _isGround = true;
            _animator.SetBool(Animator_IsJump, false);
        }
        else
        {
            _isGround = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.down * 0.38f);
    }

}
