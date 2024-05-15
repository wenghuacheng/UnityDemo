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
        //����ɾ�ʱƮ������
        [SerializeField] private Transform damagePopupTemplate;

        #region IController
        /// <summary>
        /// ��ȡ�������л�ȡ�ܹ����Ӷ���ȡModel��һϵ�е���Ϣ
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
            ///ͨ�����������ҵ����
            if (GUILayout.Button("����"))
            {
                this.SendCommand<IncreaseCountCommand>();
            }
            if (GUILayout.Button("����"))
            {
                this.SendCommand<DecreaseCountCommand>();
            }
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void Initialize()
        {
            this._countModel = this.GetModel<CounterAppModel>();

            //ע����������¼�����Commandֱ�Ӵ���ҳ�����
            this.RegisterEvent<CountChangeEvent>(CountChangeEventHandler).UnRegisterWhenGameObjectDestroyed(gameObject);
            this.RegisterEvent<AchievementEvent>(AchievementEventHandler).UnRegisterWhenGameObjectDestroyed(gameObject);


            UpdateView();
        }

        #region Event Handler
        /// <summary>
        /// ��������¼�
        /// </summary>
        private void CountChangeEventHandler(CountChangeEvent @event)
        {
            UpdateView();
        }

        /// <summary>
        /// ��óɾ�
        /// </summary>
        /// <param name="event"></param>
        private void AchievementEventHandler(AchievementEvent @event)
        {
            Vector3 worldPos = Vector3.zero;
            var damagePopup = DamagePopup.Create(damagePopupTemplate, worldPos, @event.level);
        }

        #endregion

        /// <summary>
        /// ������ͼ
        /// </summary>
        private void UpdateView()
        {
            textControl.text = this._countModel.Count.Value.ToString();
        }
    }
}