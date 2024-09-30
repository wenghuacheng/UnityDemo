using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo._01
{
    /// <summary>
    /// ֱ�ӻ�ȡ����ģʽ
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
        /// ��������
        /// </summary>
        private void KeyboardInput()
        {
            Keyboard keyboard = Keyboard.current;

            //���Input.GetKey(KeyCode.Space);
            if (keyboard != null)
            {
                if (keyboard.spaceKey.wasPressedThisFrame)
                {
                    Debug.Log("�ո��������");
                }
                if (keyboard.spaceKey.wasReleasedThisFrame)
                {
                    Debug.Log("�ո���ͷ�");
                }
                if (keyboard.spaceKey.isPressed)
                {
                    Debug.Log("�ո�����ڰ���״̬...");
                }
                if (keyboard.anyKey.wasPressedThisFrame)
                {
                    Debug.Log("�����������");
                }

            }
        }

        /// <summary>
        /// ��Ϸ�ֱ�����
        /// </summary>
        private void GamepadInput()
        {
            Gamepad gamepad = new Gamepad();
        }

        /// <summary>
        /// �������
        /// </summary>
        private void MouseInput()
        {
            Mouse mouse = Mouse.current;
            if (mouse != null)
            {
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    Debug.Log("������������");
                }
                else if (mouse.leftButton.wasReleasedThisFrame)
                {
                    Debug.Log("���������ͷ�");
                }

                var scrollValue = mouse.scroll.ReadValue();
                if (scrollValue.y != 0)
                {
                    Debug.Log("�����ֹ���:" + scrollValue.y);
                }
            }
        }
    }
}