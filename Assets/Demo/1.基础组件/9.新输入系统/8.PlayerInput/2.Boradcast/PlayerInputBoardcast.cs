using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._08
{
    public class PlayerInputBoardcast : MonoBehaviour
    {
        public void OnAttack()
        {
            Debug.Log("����Boardcast��ʽ�����İ���");
        }

        public void OnMove(InputValue value)
        {
            Debug.Log("����Boardcast��ʽ�ƶ�:" + value.Get<float>());
        }
    }
}