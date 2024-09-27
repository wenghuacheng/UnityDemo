using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo5 : MonoBehaviour
    {

        void Start()
        {
            ExecuteTimeoutDemo();
        }

        private async void ExecuteTimeoutDemo()
        {
            var cts = new CancellationTokenSource();//创建令牌
            cts.CancelAfterSlim(TimeSpan.FromSeconds(3)); // 设置5s超时

            Debug.Log("Start ExecuteTimeoutDemo");
            try
            {
                await SimpleUniTaskWithToken(cts.Token);
            }
            catch (OperationCanceledException)
            {
                //超时会触发异常
                if (cts.IsCancellationRequested)
                {
                    UnityEngine.Debug.Log("Timeout.");
                }
                else if (cts.IsCancellationRequested)
                {
                    UnityEngine.Debug.Log("Cancel clicked.");
                }
            }

            Debug.Log("End ExecuteTimeoutDemo");
        }

        private async UniTask SimpleUniTaskWithToken(CancellationToken token)
        {
            //还可以使用WithCancellation赋予token
            await UniTask.Delay(TimeSpan.FromSeconds(10000), ignoreTimeScale: false, cancellationToken: token);
            Debug.Log("End SimpleUniTaskWithToken");
        }
    }
}