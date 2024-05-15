using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// ģ����
    /// </summary>
    public class CounterAppModel : AbstractModel, ICounterAppModel
    {
        private Storage _storage;

        #region ICounterAppModel

        //���Ա��ʱ�����Զ�ˢ��
        public BindableProperty<int> Count { get; } = new BindableProperty<int>();
        #endregion

        protected override void OnInit()
        {
            _storage = this.GetUtility<Storage>();

            //���ó�ʼֵ���������¼���
            var defaultCount = _storage.LoadInt(nameof(Count));//���ش洢������
            Count.SetValueWithoutEvent(defaultCount);

            //���Ա��ʱ
            Count.Register(CountChanged);
        }

        /// <summary>
        /// �¼�����
        /// </summary>
        /// <param name="newCount"></param>
        public void CountChanged(int newCount)
        {
            _storage.SaveInt(nameof(Count), newCount);
        }

    }

}
