using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._08
{
    public class PlayerInputEventMode : MonoBehaviour
    {
        private PlayerInput input;
        private InputAction attackAction;

        private void Awake()
        {
            input = this.GetComponent<PlayerInput>();
            attackAction = input.actions.FindAction("Attack");
        }

        private void OnEnable()
        {
            attackAction.performed += AttackAction_performed;
            attackAction.Enable();
        }

        private void OnDisable()
        {
            attackAction.performed -= AttackAction_performed;
            attackAction.Disable();
        }

        private void AttackAction_performed(InputAction.CallbackContext obj)
        {
            Debug.Log("事件模式，attack触发");
        }
    }
}