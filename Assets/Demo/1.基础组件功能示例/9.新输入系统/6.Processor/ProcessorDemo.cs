using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._06
{
    public class ProcessorDemo : MonoBehaviour
    {
        public InputActionAsset actionAsset;
        private InputActionMap playerNormal;
        private InputAction moveAction;

        private void Awake()
        {
            playerNormal = actionAsset.FindActionMap("PlayerNormal");
            //获取相关的action
            moveAction = playerNormal.FindAction("Move");
        }

        private void OnEnable()
        {
            moveAction.performed += MoveAction_performed;

            playerNormal.Enable();
        }


        private void OnDisable()
        {
            moveAction.performed -= MoveAction_performed;

            playerNormal.Disable();
        }


        private void MoveAction_performed(InputAction.CallbackContext obj)
        {
            //修改过interaction后，需要达到指定的时间才会触发
            Debug.Log(obj.ReadValue<Vector2>());
        }
    }
}