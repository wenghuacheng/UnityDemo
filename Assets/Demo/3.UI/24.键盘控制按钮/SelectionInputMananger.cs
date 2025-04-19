using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.UI
{
    public class SelectionInputMananger : MonoBehaviour
    {
        public static SelectionInputMananger instance;

        public Vector2 NavigationInput { get; set; }

        private InputAction _navigationAction;

        public static PlayerInput PlayerInput;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            PlayerInput = GetComponent<PlayerInput>();
            _navigationAction = PlayerInput.actions["Navigate"];
        }


        private void Update()
        {
            NavigationInput = _navigationAction.ReadValue<Vector2>();
        }
    }
}