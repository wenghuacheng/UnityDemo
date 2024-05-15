using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// 模型对象
    /// </summary>
    public interface ICounterAppModel : IModel
    {
        BindableProperty<int> Count { get; }
    }
}