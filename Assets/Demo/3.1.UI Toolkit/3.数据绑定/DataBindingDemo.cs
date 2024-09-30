using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Demo.UIToolkit
{
    /// <summary>
    /// 数据绑定演示
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
        /// 查询所有的4个面板元素
        /// </summary>
        private void QueryPanel()
        {
            //需要与UI上的命名一致
            visualElement.Query("DetailPanel").ForEach(HandleDetailPanelChild);
        }

        /// <summary>
        /// 清除body元素中的值，使用模板替代
        /// </summary>
        private void ClearBodyPanel()
        {
            var body = visualElement.Q("BodyContainer");
            body.Clear();
        }

        /// <summary>
        /// 通过数据与模板生成内容
        /// </summary>
        private void CreatePanelContent()
        {
            var body = visualElement.Q("BodyContainer");
            foreach (var c in characcters)
            {
                var panel = new CharacterDataPanel(c);
                panel.style.flexBasis = Length.Percent(25f);//设置25%的占比
                body.Add(panel);    
            }
        }

        /// <summary>
        /// 处理单个面板中的元素
        /// </summary>
        /// <param name="panel"></param>
        private void HandleDetailPanelChild(VisualElement panel)
        {
            #region ==查询演示==
            ////可以查询其元素下的所有label【演示使用】
            //panel.Query<Label>();

            ////Q方法返回第一个匹配的元素【演示使用】
            //panel.Q<Label>();

            ////获取第X个元素【演示使用】
            //panel.Query<Label>().AtIndex(1);
            #endregion


        }
    }
}