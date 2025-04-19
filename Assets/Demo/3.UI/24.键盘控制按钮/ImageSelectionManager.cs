using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Demo.UI
{
    public class ImageSelectionManager : MonoBehaviour
    {
        public static ImageSelectionManager instance;

        public GameObject[] Cards;

        //处理因为鼠标移动后失去选择状态，无法通过键盘控制
        public GameObject LastSelected { get; set; }
        public int LastSelectedIndex { get; set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        private void OnEnable()
        {
            StartCoroutine(SetSeleectedAfterOneFrame());
        }

        /// <summary>
        /// 将第一个项目作为默认选择项
        /// 使用左右按键可以进行选择
        /// </summary>
        /// <returns></returns>
        private IEnumerator SetSeleectedAfterOneFrame()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(Cards[0]);//默认选择第一个

        }

        /// <summary>
        /// 需要将EventSysten中的InputSystem改为新的输入系统，
        /// 新建一个新的InputAction.将原来绑定的默认Inputaction中的UI节点拷贝过来
        /// </summary>
        private void Update()
        {
            if (SelectionInputMananger.instance.NavigationInput.x > 0)
            {
                //右移
                Handle(1);
            }
            else if (SelectionInputMananger.instance.NavigationInput.x < 0)
            {
                //左移
                Handle(-1);
            }
        }

        private void Handle(int addition)
        {
            if (EventSystem.current.currentSelectedGameObject == null && LastSelected != null)
            {
                int newIndex = LastSelectedIndex + addition;
                newIndex = math.clamp(newIndex, 0, Cards.Length - 1);
                EventSystem.current.SetSelectedGameObject(Cards[newIndex]);
            }
        }
    }
}