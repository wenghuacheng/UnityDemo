using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._08
{
    /// <summary>
    /// PlayerInput����SendMessage��ʽ
    /// </summary>
    public class PlayerInputSendMessage : MonoBehaviour
    {
        public void OnAttack()
        {
            Debug.Log("����SendMessage��ʽ�����İ���");
        }

        public void OnMove(InputValue value)
        {
            Debug.Log("����SendMessage��ʽ�ƶ�:" + value.Get<float>());
        }
    }
}