using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnimatorDemo
{
    public class SimpleAnimatorControl : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Idle"))
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Jump", false);
            }
            if (GUI.Button(new Rect(0, 30, 100, 30), "Walk"))
            {
                animator.SetBool("Walking", true);
                animator.SetBool("Jump", false);
            }
            if (GUI.Button(new Rect(0, 60, 100, 30), "Jump"))
            {
                animator.SetBool("Walking", false);
                animator.SetBool("Jump", true);
            }
        }
    }
}