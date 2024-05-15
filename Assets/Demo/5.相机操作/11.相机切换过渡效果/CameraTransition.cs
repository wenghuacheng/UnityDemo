using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.CustomCamera.Transition
{
    public class CameraTransition : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera[] cameras;

        private int curIndex = 0;
        private int highPriority = 5;

        void Start()
        {
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].Priority = 0;
            }
            cameras[curIndex].Priority = highPriority;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                //µã»÷ÇÐ»»Ïà»ú
                curIndex++;
                curIndex = curIndex % cameras.Length;
                for (int i = 0; i < cameras.Length; i++)
                {
                    if (i == curIndex)
                        cameras[i].Priority = highPriority;
                    else
                        cameras[i].Priority = 0;
                }
            }

        }
    }
}