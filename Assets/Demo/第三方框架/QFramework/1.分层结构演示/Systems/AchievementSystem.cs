using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// 成就系统【独立模块】
    /// </summary>
    public class AchievementSystem : AbstractSystem
    {
        private CounterAppModel _countModel;

        protected override void OnInit()
        {
            //获取数据对象
            _countModel = this.GetModel<CounterAppModel>();
            //注册事件处理
            this.RegisterEvent<CountChangeEvent>(CountChangeEventHander);
        }

        /// <summary>
        /// 事件处理
        /// </summary>
        /// <param name="event"></param>
        private void CountChangeEventHander(CountChangeEvent @event)
        {
            //通知界面处理
        }
    }
}