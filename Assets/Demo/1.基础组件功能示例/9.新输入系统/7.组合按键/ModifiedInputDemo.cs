using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._07
{
    public class ModifiedInputDemo : MonoBehaviour
    {
        public InputActionAsset asset;
        private InputActionMap map;
        private InputAction action;

        private void Awake()
        {
            map = asset.FindActionMap("Normal");
            action = map.FindAction("Attack");
        }

        private void OnEnable()
        {
            action.performed += Action_performed;
            map.Enable();
        }
        private void OnDisable()
        {
            action.performed -= Action_performed;
            map.Disable();
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            Debug.Log("°´¼ü´¥·¢");
        }
    }
}
