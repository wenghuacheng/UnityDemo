using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// ������ͬ�Ĺ������Ƿ�ᱻ�ظ�ע��
    /// </summary>
    public class CounterAppController2 : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}