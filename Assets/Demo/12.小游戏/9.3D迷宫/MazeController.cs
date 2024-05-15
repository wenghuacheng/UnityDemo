using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Games.Maze
{
    public class MazeController : MonoBehaviour
    {
        [SerializeField] private float maxRotation = 20;

        private float rotationZ = 0;
        private float rotationX = 0;

        void Start()
        {

        }

        void Update()
        {
            //鼠标位移
            rotationZ += Input.GetAxis("Mouse X");
            rotationX += Input.GetAxis("Mouse Y");

            rotationZ = Mathf.Clamp(rotationZ, -maxRotation, maxRotation);
            rotationX = Mathf.Clamp(rotationX, -maxRotation, maxRotation);

            //基于鼠标移动旋转迷宫
            this.transform.rotation = Quaternion.Euler(rotationX, 0, -rotationZ);
        }
    }
}