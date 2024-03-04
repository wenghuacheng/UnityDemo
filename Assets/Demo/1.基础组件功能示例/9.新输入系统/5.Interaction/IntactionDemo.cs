using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._05
{
    public class IntactionDemo : MonoBehaviour
    {
        public InputActionAsset actionAsset;
        private InputActionMap playerNormal;
        private InputAction jumpAction;

        private void Awake()
        {
            playerNormal = actionAsset.FindActionMap("PlayerNormal");
            //获取相关的action
            jumpAction = playerNormal.FindAction("Jump");
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
            //修改过interaction后，需要达到指定的时间才会触发
            Debug.Log("跳跃键被按下");
        }

        private void JumpAction_canceled(InputAction.CallbackContext obj)
        {
            Debug.Log("跳跃键释放");
        }
    }
}