using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._09
{
    public class RebindDemo : MonoBehaviour
    {
        public InputActionAsset asset;
        private InputActionMap map;
        private InputAction moveAction;
        private InputAction attackAction;

        private void Awake()
        {
            map = asset.FindActionMap("Normal");
            moveAction = map.FindAction("Move");
            attackAction = map.FindAction("Attack");
        }

        private void OnEnable()
        {
            moveAction.performed += MoveAction_performed;
            attackAction.performed += AttackAction_performed;
            map.Enable();
        }
        private void OnDisable()
        {
            moveAction.performed -= MoveAction_performed;
            attackAction.performed -= AttackAction_performed;
            map.Disable();
        }


        private void MoveAction_performed(InputAction.CallbackContext obj)
        {
            Debug.Log("ÒÆ¶¯:" + obj.ReadValue<Vector2>());
        }


        private void AttackAction_performed(InputAction.CallbackContext obj)
        {
            Debug.Log("¹¥»÷");
        }
    }
}