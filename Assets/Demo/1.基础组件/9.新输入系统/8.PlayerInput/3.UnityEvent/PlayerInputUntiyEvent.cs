using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Demo.Basic.InputDemo._08
{
    /// <summary>
    /// unity事件
    /// </summary>
    public class PlayerInputUntiyEvent : MonoBehaviour
    {
        public void OnAttack()
        {
            Debug.Log("基于UntiyEvent方式触发的按键");
        }

        public void OnMove(CallbackContext value)
        {
            Debug.Log("基于UntiyEvent方式移动:" + value.ReadValue<float>());
        }
    }
}