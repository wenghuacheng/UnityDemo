using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Basic.InputDemo
{
    public class PlayerController : MonoBehaviour
    {
        //ͨ��������SO������������������ϵͳǿ��
        [SerializeField] private InputReader input;

        void Start()
        {
            input.StartEvent += Input_StartEvent;
            input.PauseEvent += Input_PauseEvent;
            input.MoveEvent += Input_MoveEvent;
        }

        #region Event Handler
        private void Input_MoveEvent(Vector2 obj)
        {
            Debug.Log("Move:" + obj);
        }

        private void Input_PauseEvent()
        {
            Debug.Log("Input_PauseEvent");
        }

        private void Input_StartEvent()
        {
            Debug.Log("Input_StartEvent");
        }

        #endregion
    }
}