using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.UIToolkit
{
    /// <summary>
    /// ���ݰ���ʾ
    /// </summary>
    public class DataBindingDemo : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        [SerializeField] private CharaccterData[] characcters;

        private VisualElement visualElement;

        private void Start()
        {
            visualElement = document.rootVisualElement;
            //QueryPanel();
            ClearBodyPanel();
            CreatePanelContent();
        }

        /// <summary>
        /// ��ѯ���е�4�����Ԫ��
        /// </summary>
        private void QueryPanel()
        {
            //��Ҫ��UI�ϵ�����һ��
            visualElement.Query("DetailPanel").ForEach(HandleDetailPanelChild);
        }

        /// <summary>
        /// ���bodyԪ���е�ֵ��ʹ��ģ�����
        /// </summary>
        private void ClearBodyPanel()
        {
            var body = visualElement.Q("BodyContainer");
            body.Clear();
        }

        /// <summary>
        /// ͨ��������ģ����������
        /// </summary>
        private void CreatePanelContent()
        {
            var body = visualElement.Q("BodyContainer");
            foreach (var c in characcters)
            {
                var panel = new CharacterDataPanel(c);
                panel.style.flexBasis = Length.Percent(25f);//����25%��ռ��
                body.Add(panel);    
            }
        }

        /// <summary>
        /// ����������е�Ԫ��
        /// </summary>
        /// <param name="panel"></param>
        private void HandleDetailPanelChild(VisualElement panel)
        {
            #region ==��ѯ��ʾ==
            ////���Բ�ѯ��Ԫ���µ�����label����ʾʹ�á�
            //panel.Query<Label>();

            ////Q�������ص�һ��ƥ���Ԫ�ء���ʾʹ�á�
            //panel.Q<Label>();

            ////��ȡ��X��Ԫ�ء���ʾʹ�á�
            //panel.Query<Label>().AtIndex(1);
            #endregion


        }
    }
}