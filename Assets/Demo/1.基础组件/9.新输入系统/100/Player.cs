using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputResetDemo
{
    public class Player : MonoBehaviour, GameInput.IResetGameInputActions
    {
        private GameInput _input;

        private void Awake()
        {
            //只是演示重置，这里直接与player绑定
            _input = new GameInput();
            _input.ResetGameInput.SetCallbacks(this);
            _input.Enable();
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        #region IResetGameInputActions
        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<Vector2>());
        }
        #endregion

    }
}