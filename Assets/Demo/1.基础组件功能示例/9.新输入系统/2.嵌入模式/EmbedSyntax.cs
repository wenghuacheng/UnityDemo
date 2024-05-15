using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._02
{
    /// <summary>
    /// Ƕ��ģʽ
    /// </summary>
    public class EmbedSyntax : MonoBehaviour
    {
        public InputAction testAction;

        private void OnEnable()
        {
            testAction.performed += TestAction_performed;
            testAction.canceled += TestAction_canceled;
            testAction.Enable();
        }

        private void OnDisable()
        {
            testAction.performed -= TestAction_performed;
            testAction.canceled -= TestAction_canceled;
            testAction.Disable();
        }


        private void TestAction_performed(InputAction.CallbackContext obj)
        {
            var bl = obj.ReadValueAsButton();
            Debug.Log("�����ѱ�����");
        }


        private void TestAction_canceled(InputAction.CallbackContext obj)
        {
            Debug.Log("�����ѱ��ͷ�");
        }

    }
}