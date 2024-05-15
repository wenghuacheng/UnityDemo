using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._01
{
    /// <summary>
    /// 直接获取按键模式
    /// </summary>
    public class DirectWork : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            KeyboardInput();
            GamepadInput();
            MouseInput();
        }

        /// <summary>
        /// 键盘输入
        /// </summary>
        private void KeyboardInput()
        {
            Keyboard keyboard = Keyboard.current;

            //替代Input.GetKey(KeyCode.Space);
            if (keyboard != null)
            {
                if (keyboard.spaceKey.wasPressedThisFrame)
                {
                    Debug.Log("空格键被按下");
                }
                if (keyboard.spaceKey.wasReleasedThisFrame)
                {
                    Debug.Log("空格键释放");
                }
                if (keyboard.spaceKey.isPressed)
                {
                    Debug.Log("空格键处于按下状态...");
                }
                if (keyboard.anyKey.wasPressedThisFrame)
                {
                    Debug.Log("任意键被按下");
                }

            }
        }

        /// <summary>
        /// 游戏手柄输入
        /// </summary>
        private void GamepadInput()
        {
            Gamepad gamepad = new Gamepad();
        }

        /// <summary>
        /// 鼠标输入
        /// </summary>
        private void MouseInput()
        {
            Mouse mouse = Mouse.current;
            if (mouse != null)
            {
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    Debug.Log("鼠标左键被按下");
                }
                else if (mouse.leftButton.wasReleasedThisFrame)
                {
                    Debug.Log("鼠标左键被释放");
                }

                var scrollValue = mouse.scroll.ReadValue();
                if (scrollValue.y != 0)
                {
                    Debug.Log("鼠标滚轮滚动:" + scrollValue.y);
                }
            }
        }
    }
}