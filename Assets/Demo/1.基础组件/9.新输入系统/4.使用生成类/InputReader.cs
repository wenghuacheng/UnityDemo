using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo
{
    /// <summary>
    /// 通过创建SO的方式。而不是直接将输入系统和玩家强制绑定
    /// </summary>
    [CreateAssetMenu(menuName = "测试SO/输入系统示例")]
    public class InputReader : ScriptableObject, DemoGameInput.IGameInputActions, DemoGameInput.IUIActions
    {
        //按键事件
        public event Action<Vector2> MoveEvent;
        public event Action StartEvent;
        public event Action PauseEvent;

        private DemoGameInput gameInput;

        private void OnEnable()
        {
            if (gameInput == null)
            {
                gameInput = new DemoGameInput();
                //注册输入系统回调的处理类
                gameInput.GameInput.SetCallbacks(this);
                gameInput.UI.SetCallbacks(this);

                //启用事件。
                gameInput.GameInput.Enable();
                gameInput.UI.Enable();

                //PS:真实游戏中显示UI菜单时会忽略游戏中的输入。通过Disable方法禁用
                //【不能显示菜单时玩家还能操作角色】
                //gameInput.GameInput.Disable();
                //gameInput.UI.Enable();
            }
        }

        #region 输入系统中设置的Move相关事件
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
        #endregion

        #region 输入系统中设置的UI相关事件
        public void OnPause(InputAction.CallbackContext context)
        {
            //注意：针对button类型按键时会有多种响应，这样只要响应Performed
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
            }
        }

        public void OnStart(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                StartEvent?.Invoke();
            }
        }
        #endregion
    }
}