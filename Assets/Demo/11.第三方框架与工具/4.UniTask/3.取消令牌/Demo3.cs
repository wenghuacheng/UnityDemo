using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo3 : MonoBehaviour
    {
        void Start()
        {
            Task.Run(() =>
            {
                ExecuteCancellationTokenSourceDemo();
            });
        }

        private void ExecuteCancellationTokenSourceDemo()
        {
            var cts = new CancellationTokenSource();//创建令牌

            Debug.Log("Start ExecuteCancellationTokenSourceDemo");
            SimpleUniTaskWithToken(cts.Token);

            //等待2s后取消任务
            Thread.Sleep(2000);
            Debug.Log("--时间到，取消任务");
            cts.Cancel();

            Debug.Log("End ExecuteCancellationTokenSourceDemo");
        }

        private async UniTask SimpleUniTaskWithToken(CancellationToken token)
        {
            //还可以使用WithCancellation赋予token
            await UniTask.Delay(TimeSpan.FromSeconds(10000), ignoreTimeScale: false, cancellationToken: token);
            Debug.Log("End SimpleUniTaskWithToken");
        }
    }
}