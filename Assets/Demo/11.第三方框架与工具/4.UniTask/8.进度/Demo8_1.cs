using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Demo.Tools.UniTaskDemo
{
    /// <summary>
    /// 进度接口
    /// </summary>
    public class Demo8_1 : MonoBehaviour, IProgress<float>
    {
        private void Awake()
        {
            WebRequest().Forget();
        }

        public void Report(float value)
        {
            UnityEngine.Debug.Log(value);
        }

        public async UniTaskVoid WebRequest()
        {
            var request = await UnityWebRequest.Get("http://www.baidu.com")
                .SendWebRequest()
                .ToUniTask(progress: this);

            Debug.Log(request.result);
        }
    }
}