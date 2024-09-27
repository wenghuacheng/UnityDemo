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
            //ʹ��TimeoutController �Ż���ʱ
            TimeoutController timeoutController = new TimeoutController(); // ����timeoutController

            #region ����ʾ������������Դһ��ʹ��
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
                timeoutController.Reset(); // ��await��ɺ����Reset��ֹͣ��ʱ��ʱ������׼����һ�θ��ã�
            }

            Debug.Log("end ExecuteTimeoutControllerDemo");
        }

        private async UniTask SimpleUniTaskWithToken(CancellationToken token)
        {
            //������ʹ��WithCancellation����token
            await UniTask.Delay(TimeSpan.FromSeconds(10000), ignoreTimeScale: false, cancellationToken: token);
            Debug.Log("End SimpleUniTaskWithToken");
        }
    }
}