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
        private Storage _storage;

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
            //todo：到达成就时通过Storage进行记录

            if (_countModel.Count.Value % 10 == 0)
            {
                this.SendEvent<AchievementEvent>(new AchievementEvent() { level = _countModel.Count.Value });
            }

        }
    }
}