using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// 增加命令
    /// </summary>
    public class IncreaseCountCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<CounterAppModel>().Count.Value++;

            //触发数量变更事件
            this.SendEvent<CountChangeEvent>();
        }
    }
}