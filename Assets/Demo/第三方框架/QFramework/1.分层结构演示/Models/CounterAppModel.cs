using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// 模型类
    /// </summary>
    public class CounterAppModel : AbstractModel, ICounterAppModel
    {
        private Storage _storage;

        #region ICounterAppModel

        //属性变更时可以自动刷新
        public BindableProperty<int> Count { get; } = new BindableProperty<int>();
        #endregion

        protected override void OnInit()
        {
            _storage = this.GetUtility<Storage>();

            //设置初始值（不触发事件）
            var defaultCount = _storage.LoadInt(nameof(Count));//加载存储的数据
            Count.SetValueWithoutEvent(defaultCount);

            //属性变更时
            Count.Register(CountChanged);
        }

        /// <summary>
        /// 事件更新
        /// </summary>
        /// <param name="newCount"></param>
        public void CountChanged(int newCount)
        {
            _storage.SaveInt(nameof(Count), newCount);
        }

    }

}
