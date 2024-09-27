using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    /// <summary>
    /// 令牌生命周期控制
    /// </summary>
    public class Demo4 : MonoBehaviour
    {
        CancellationTokenSource disableCancellation = new CancellationTokenSource();
        CancellationTokenSource destroyCancellation = new CancellationTokenSource();

        private void Start()
        {
            //在运行过程中将脚本禁用，会触发disableCancellation

            Task.Run(async () =>
            {
                try
                {
                    //触发token的cancel后会引发异常
                    await SimpleUniTaskWithToken(disableCancellation.Token);
                }
                catch (Exception ex)
                {
                    Debug.Log("异常：" + ex.Message);
                }

                Debug.Log("结束");
            });
        }

        private async UniTask SimpleUniTaskWithToken(CancellationToken token)
        {
            //还可以使用WithCancellation赋予token
            Debug.Log("Start SimpleUniTaskWithToken");
            await UniTask.Delay(TimeSpan.FromSeconds(10000), ignoreTimeScale: false, cancellationToken: token);
            Debug.Log("End SimpleUniTaskWithToken");
        }


        private void OnEnable()
        {
            if (disableCancellation != null)
            {
                disableCancellation.Dispose();
            }
            disableCancellation = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            disableCancellation.Cancel();
        }

        private void OnDestroy()
        {
            destroyCancellation.Cancel();
            destroyCancellation.Dispose();
        }
    }
}