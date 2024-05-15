using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// ��������
    /// </summary>
    public class IncreaseCountCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<CounterAppModel>().Count.Value++;

            //������������¼�
            this.SendEvent<CountChangeEvent>();
        }
    }
}