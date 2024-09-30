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
        //Action名
        public TextMeshProUGUI actionLabel;
        //Binding名
        public TextMeshProUGUI bindingLabel;
        //Binding的id
        public string bindingId;

        private int index;
        [SerializeField]
        public InputActionReference actionReference;
        private InputActionRebindingExtensions.RebindingOperation RebindOperation;

        void Start()
        {
            //UI显示Action名
            actionLabel.text = actionReference.name;

            //获取BingdingID
            /* 这里只是为了简单，所以手动选择要修改的binding，并且获取它的id
             * 这个例子里面，第0层是WASD
             * 第1层是composite的第1个部分W
             * 第2层是composite的第2个部分S
             * 第3层是composite的第3个部分A
             * 第4层是composite的第4个部分D
             * 第5层是左摇杆
             */
            index = 0;
            bindingId = actionReference.action.bindings[index].id.ToString();
            //UI刷新Binding名
            UpdateLabel(index);
        }

        /// <summary>
        /// 开始替换
        /// </summary>
        /// <param name="index"></param>
        public void StartInteractiveRebind()
        {
            //获取bindingIndex，Bingdings数组的下标，从0开始，如果action为空返回false
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
        /// 获取bindingIndex，Bingdings数组的下标，从0开始，如果action为空返回false
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
                .WithControlsExcluding("Mouse")//剔除鼠标
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
                        Debug.Log("设置完成");
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
        /// UI刷新binding名
        /// </summary>
        /// <param name="index"></param>
        private void UpdateLabel(int index)
        {
            bindingLabel.text = actionReference.action.GetBindingDisplayString(index, out string deviceLayoutName, out string controlPath);
        }

    }
}