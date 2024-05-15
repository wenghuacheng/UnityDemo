using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// ģ�Ͷ���
    /// </summary>
    public interface ICounterAppModel : IModel
    {
        BindableProperty<int> Count { get; }
    }
}