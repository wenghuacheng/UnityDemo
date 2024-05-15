using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Demo.Basic.InputDemo._08
{
    /// <summary>
    /// unity�¼�
    /// </summary>
    public class PlayerInputUntiyEvent : MonoBehaviour
    {
        public void OnAttack()
        {
            Debug.Log("����UntiyEvent��ʽ�����İ���");
        }

        public void OnMove(CallbackContext value)
        {
            Debug.Log("����UntiyEvent��ʽ�ƶ�:" + value.ReadValue<float>());
        }
    }
}