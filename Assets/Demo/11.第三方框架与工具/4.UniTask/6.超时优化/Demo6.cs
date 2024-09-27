using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo6 : MonoBehaviour
    {

        void Start()
        {
            ExecuteTimeoutControllerDemo();
        }

        private async void ExecuteTimeoutControllerDemo()
        {
            //使用TimeoutController 优化超时
            TimeoutController timeoutController = new TimeoutController(); // 复用timeoutController

            #region 【演示】可以与其他源一起使用
            //var clickCancelSource1 = new CancellationTokenSource();
            //var timeoutController1 = new TimeoutController(clickCancelSource1);
            #endregion

            Debug.Log("start ExecuteTimeoutControllerDemo");
            try
            {
                await SimpleUniTaskWithToken(timeoutController.Timeout(TimeSpan.FromSeconds(5)));
            }
            catch (OperationCanceledException)
            {
                if (timeoutController.IsTimeout())
                {
                    UnityEngine.Debug.Log("timeout");
                }
            }
            finally
            {
                timeoutController.Reset(); // 当await完成后调用Reset（停止超时计时器，并准备下一次复用）
            }

            Debug.Log("end ExecuteTimeoutControllerDemo");
        }

        private async UniTask SimpleUniTaskWithToken(CancellationToken token)
        {
            //还可以使用WithCancellation赋予token
            await UniTask.Delay(TimeSpan.FromSeconds(10000), ignoreTimeScale: false, cancellationToken: token);
            Debug.Log("End SimpleUniTaskWithToken");
        }
    }
}