using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._03
{
    /// <summary>
    /// �����Inputaction
    /// </summary>
    public class GatherInput : MonoBehaviour
    {
        public InputActionAsset actionAsset;

        private InputActionMap playerNormal;
        private InputAction jumpAction;
        private InputAction moveAction;

        private void Awake()
        {
            playerNormal = actionAsset.FindActionMap("PlayerNormal");
            //��ȡ��ص�action
            jumpAction = playerNormal.FindAction("Jump");
            moveAction = playerNormal.FindAction("MoveHorizontal");
        }

        private void OnEnable()
        {
            jumpAction.performed += JumpAction_performed;
            jumpAction.canceled += JumpAction_canceled;

            playerNormal.Enable();
        }


        private void OnDisable()
        {
            jumpAction.performed -= JumpAction_performed;
            jumpAction.canceled -= JumpAction_canceled;

            playerNormal.Disable();
        }


        private void JumpAction_performed(InputAction.CallbackContext obj)
        {
            Debug.Log("��Ծ��������");   
        }

        private void JumpAction_canceled(InputAction.CallbackContext obj)
        {
            Debug.Log("��Ծ���ͷ�");
        }

    }
}