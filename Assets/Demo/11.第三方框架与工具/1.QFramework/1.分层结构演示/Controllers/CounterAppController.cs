using Demo.UI;
using QFramework;
using QFramework.Demo.Layers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.Demo.Layers
{
    public class CounterAppController : MonoBehaviour, IController
    {
        private CounterAppModel _countModel;

        [SerializeField] private TextMeshProUGUI textControl;
        //到达成就时飘出数字
        [SerializeField] private Transform damagePopupTemplate;

        #region IController
        /// <summary>
        /// 获取从容器中获取架构，从而获取Model等一系列的信息
        /// </summary>
        /// <returns></returns>
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }

        #endregion

        #region MonoBehaviour

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {

        }
        #endregion

        private void OnGUI()
        {
            ///通过发送命令触发业务处理
            if (GUILayout.Button("增加"))
            {
                this.SendCommand<IncreaseCountCommand>();
            }
            if (GUILayout.Button("减少"))
            {
                this.SendCommand<DecreaseCountCommand>();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            this._countModel = this.GetModel<CounterAppModel>();

            //注册数量变更事件，由Command直接触发页面更新
            this.RegisterEvent<CountChangeEvent>(CountChangeEventHandler).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<AchievementEvent>(AchievementEventHandler).UnRegisterWhenGameObjectDestroyed(gameObject);


            UpdateView();
        }

        #region Event Handler
        /// <summary>
        /// 数量变更事件
        /// </summary>
        private void CountChangeEventHandler(CountChangeEvent @event)
        {
            UpdateView();
        }

        /// <summary>
        /// 获得成就
        /// </summary>
        /// <param name="event"></param>
        private void AchievementEventHandler(AchievementEvent @event)
        {
            Vector3 worldPos = Vector3.zero;
            var damagePopup = DamagePopup.Create(damagePopupTemplate, worldPos, @event.level);
        }

        #endregion

        /// <summary>
        /// 更新视图
        /// </summary>
        private void UpdateView()
        {
            textControl.text = this._countModel.Count.Value.ToString();
        }
    }
}