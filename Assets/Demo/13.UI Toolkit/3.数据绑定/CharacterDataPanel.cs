using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.UIToolkit
{
    /// <summary>
    /// ��ɫ��������ࡾ�Զ���ؼ���
    /// </summary>
    public class CharacterDataPanel : VisualElement
    {
        //���౩¶��UXML�ļ��У�����������UI Builder��
        public new class UxmlFactory : UxmlFactory<CharacterDataPanel> { }

        private TemplateContainer templateContainer;

        public CharacterDataPanel()
        {
            //��Ҫģ����Ҫ����Resouces�ļ�����
            templateContainer = Resources.Load<VisualTreeAsset>("DetailPanel").Instantiate();
            templateContainer.style.flexGrow = 1;

            hierarchy.Add(templateContainer);
        }

        public CharacterDataPanel(CharaccterData data) : this()
        {
            userData = data;

            templateContainer.Q("Avatar").style.backgroundImage = data.avatar;
            templateContainer.Q<Label>("NameLabel").text = data.characterName;

            //ͨ��������������������
            Clickable clickable = new Clickable(OnClick);
            //�޸Ĵ�����Ϊ�Ҽ�
            clickable.activators.Clear();
            clickable.activators.Add(new ManipulatorActivationFilter() { button= MouseButton.RightMouse });
            templateContainer.AddManipulator(clickable);

            //ͨ���¼�ϵͳ������������
            templateContainer.RegisterCallback<MouseDownEvent>(OnClick);
        }

        /// <summary>
        /// �����Ϊ
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