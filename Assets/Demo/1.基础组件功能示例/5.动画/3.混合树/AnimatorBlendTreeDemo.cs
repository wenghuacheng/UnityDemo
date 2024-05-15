using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimatorDemo
{
    public class AnimatorBlendTreeDemo : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody2D rb;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Vector2.up * 5;
            }

            //通过速度判断是否是上升还是下降
            animator.SetFloat("yVelocity", rb.velocity.y);
            animator.SetBool("Jump", rb.velocity.y != 0f);
        }
    }
}