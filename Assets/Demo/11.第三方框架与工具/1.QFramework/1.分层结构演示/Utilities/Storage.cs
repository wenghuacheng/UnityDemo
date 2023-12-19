using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// 数据存储类
    /// 可以基于一个通用的数据存储接口，后面可以变更为其他存储库如EasySave的
    /// </summary>
    public class Storage : IUtility
    {
        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int LoadInt(string key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
    }
}