using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo.Basic.InputDemo
{
    /// <summary>
    /// ͨ������SO�ķ�ʽ��������ֱ�ӽ�����ϵͳ�����ǿ�ư�
    /// </summary>
    [CreateAssetMenu(menuName = "����SO/����ϵͳʾ��")]
    public class InputReader : ScriptableObject, DemoGameInput.IGameInputActions, DemoGameInput.IUIActions
    {
        //�����¼�
        public event Action<Vector2> MoveEvent;
        public event Action StartEvent;
        public event Action PauseEvent;

        private DemoGameInput gameInput;

        private void OnEnable()
        {
            if (gameInput == null)
            {
                gameInput = new DemoGameInput();
                //ע������ϵͳ�ص��Ĵ�����
                gameInput.GameInput.SetCallbacks(this);
                gameInput.UI.SetCallbacks(this);

                //�����¼���
                gameInput.GameInput.Enable();
                gameInput.UI.Enable();

                //PS:��ʵ��Ϸ����ʾUI�˵�ʱ�������Ϸ�е����롣ͨ��Disable��������
                //��������ʾ�˵�ʱ��һ��ܲ�����ɫ��
                //gameInput.GameInput.Disable();
                //gameInput.UI.Enable();
            }
        }

        #region ����ϵͳ�����õ�Move����¼�
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent?.Invoke(context.ReadValue<Vector2>());
        }
        #endregion

        #region ����ϵͳ�����õ�UI����¼�
        public void OnPause(InputAction.CallbackContext context)
        {
            //ע�⣺���button���Ͱ���ʱ���ж�����Ӧ������ֻҪ��ӦPerformed
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