using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Demo.Basic.InputResetDemo
{
    public class RebindKeyboard : MonoBehaviour
    {
        //Action��
        public TextMeshProUGUI actionLabel;
        //Binding��
        public TextMeshProUGUI bindingLabel;
        //Binding��id
        public string bindingId;

        private int index;
        [SerializeField]
        public InputActionReference actionReference;
        private InputActionRebindingExtensions.RebindingOperation RebindOperation;

        void Start()
        {
            //UI��ʾAction��
            actionLabel.text = actionReference.name;

            //��ȡBingdingID
            /* ����ֻ��Ϊ�˼򵥣������ֶ�ѡ��Ҫ�޸ĵ�binding�����һ�ȡ����id
             * ����������棬��0����WASD
             * ��1����composite�ĵ�1������W
             * ��2����composite�ĵ�2������S
             * ��3����composite�ĵ�3������A
             * ��4����composite�ĵ�4������D
             * ��5������ҡ��
             */
            index = 0;
            bindingId = actionReference.action.bindings[index].id.ToString();
            //UIˢ��Binding��
            UpdateLabel(index);
        }

        /// <summary>
        /// ��ʼ�滻
        /// </summary>
        /// <param name="index"></param>
        public void StartInteractiveRebind()
        {
            //��ȡbindingIndex��Bingdings������±꣬��0��ʼ�����actionΪ�շ���false
            if (!CheckActionAndBinding(out int bindingIndex))
                return;

            // If the binding is a composite, we need to rebind each part in turn.
            if (actionReference.action.bindings[bindingIndex].isComposite)
            {
                var firstPartIndex = bindingIndex + 1;
                if (firstPartIndex < actionReference.action.bindings.Count && actionReference.action.bindings[firstPartIndex].isPartOfComposite)
                    PerformInteractiveRebind(actionReference, firstPartIndex, allCompositeParts: true);
            }
            else
            {
                PerformInteractiveRebind(actionReference, bindingIndex);
            }
        }

        /// <summary>
        /// ��ȡbindingIndex��Bingdings������±꣬��0��ʼ�����actionΪ�շ���false
        /// </summary>
        /// <param name="index"></param>
        private bool CheckActionAndBinding(out int bindingIndex)
        {
            bindingIndex = -1;
            if (actionReference == null)
                return false;
            bindingIndex = actionReference.action.bindings.IndexOf(x => x.id == new System.Guid(bindingId));
            return true;

        }

        private void PerformInteractiveRebind(InputAction action, int bindingIndex, bool allCompositeParts = false)
        {
            RebindOperation?.Cancel(); // Will null out m_RebindOperation.

            void CleanUp()
            {
                RebindOperation?.Dispose();
                RebindOperation = null;
            }

            // Configure the rebind.
            RebindOperation = action.PerformInteractiveRebinding(bindingIndex)
                .WithControlsExcluding("Mouse")//�޳����
                .OnCancel(
                    operation =>
                    {
                        CleanUp();
                    })
                .OnComplete(
                    operation =>
                    {
                        UpdateLabel(index);
                        CleanUp();
                        Debug.Log("�������");
                    // If there's more composite parts we should bind, initiate a rebind
                    // for the next part.
                    if (allCompositeParts)
                        {
                            var nextBindingIndex = bindingIndex + 1;
                            if (nextBindingIndex < action.bindings.Count && action.bindings[nextBindingIndex].isPartOfComposite)
                                PerformInteractiveRebind(action, nextBindingIndex, true);
                        }
                    });
            RebindOperation.Start();
        }

        /// <summary>
        /// UIˢ��binding��
        /// </summary>
        /// <param name="index"></param>
        private void UpdateLabel(int index)
        {
            bindingLabel.text = actionReference.action.GetBindingDisplayString(index, out string deviceLayoutName, out string controlPath);
        }

    }
}