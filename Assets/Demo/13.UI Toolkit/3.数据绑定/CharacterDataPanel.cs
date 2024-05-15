using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.UIToolkit
{
    /// <summary>
    /// 角色数据面板类【自定义控件】
    /// </summary>
    public class CharacterDataPanel : VisualElement
    {
        //让类暴露到UXML文件中，该类会出现在UI Builder中
        public new class UxmlFactory : UxmlFactory<CharacterDataPanel> { }

        private TemplateContainer templateContainer;

        public CharacterDataPanel()
        {
            //主要模板需要放在Resouces文件夹下
            templateContainer = Resources.Load<VisualTreeAsset>("DetailPanel").Instantiate();
            templateContainer.style.flexGrow = 1;

            hierarchy.Add(templateContainer);
        }

        public CharacterDataPanel(CharaccterData data) : this()
        {
            userData = data;

            templateContainer.Q("Avatar").style.backgroundImage = data.avatar;
            templateContainer.Q<Label>("NameLabel").text = data.characterName;

            //通过操作器触发按键反馈
            Clickable clickable = new Clickable(OnClick);
            //修改触发器为右键
            clickable.activators.Clear();
            clickable.activators.Add(new ManipulatorActivationFilter() { button= MouseButton.RightMouse });
            templateContainer.AddManipulator(clickable);

            //通过事件系统触发按键反馈
            templateContainer.RegisterCallback<MouseDownEvent>(OnClick);
        }

        /// <summary>
        /// 点击行为
        /// </summary>
        /// <param name="clickEvent"></param>
        private void OnClick(EventBase clickEvent)
        {
            ((CharaccterData)userData).characterName += "1";
            templateContainer.Q<Label>("NameLabel").text = ((CharaccterData)userData).characterName;

        }

        private void OnClick(MouseDownEvent clickEvent)
        {
            ((CharaccterData)userData).characterName += "1";
            templateContainer.Q<Label>("NameLabel").text = ((CharaccterData)userData).characterName;

        }
    }
}