using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._08
{
    /// <summary>
    /// PlayerInput基于SendMessage方式
    /// </summary>
    public class PlayerInputSendMessage : MonoBehaviour
    {
        public void OnAttack()
        {
            Debug.Log("基于SendMessage方式触发的按键");
        }

        public void OnMove(InputValue value)
        {
            Debug.Log("基于SendMessage方式移动:" + value.Get<float>());
        }
    }
}