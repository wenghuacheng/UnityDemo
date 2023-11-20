using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// 测试相同的管理类是否会被重复注册
    /// </summary>
    public class CounterAppController2 : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}