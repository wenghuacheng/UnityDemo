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
            //��ȡ��ص�action
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
            //�޸Ĺ�interaction����Ҫ�ﵽָ����ʱ��Żᴥ��
            Debug.Log(obj.ReadValue<Vector2>());
        }
    }
}