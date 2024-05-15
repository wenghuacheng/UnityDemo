using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// �ɾ�ϵͳ������ģ�顿
    /// </summary>
    public class AchievementSystem : AbstractSystem
    {
        private CounterAppModel _countModel;
        private Storage _storage;

        protected override void OnInit()
        {
            //��ȡ���ݶ���
            _countModel = this.GetModel<CounterAppModel>();
            //ע���¼�����
            this.RegisterEvent<CountChangeEvent>(CountChangeEventHander);
        }

        /// <summary>
        /// �¼�����
        /// </summary>
        /// <param name="event"></param>
        private void CountChangeEventHander(CountChangeEvent @event)
        {
            //todo������ɾ�ʱͨ��Storage���м�¼

            if (_countModel.Count.Value % 10 == 0)
            {
                this.SendEvent<AchievementEvent>(new AchievementEvent() { level = _countModel.Count.Value });
            }

        }
    }
}