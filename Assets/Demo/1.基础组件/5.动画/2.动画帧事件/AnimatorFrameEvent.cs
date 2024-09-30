using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimatorDemo
{
    public class AnimatorFrameEvent : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Bigger"))
            {
                animator.SetTrigger("Bigger");
            }
        }

        /// <summary>
        /// ��󶯻�����
        /// </summary>
        public void BiggerEnd()
        {
            Debug.Log("������֡����");
        }
    }
}