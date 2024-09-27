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
            var cts = new CancellationTokenSource();//��������

            Debug.Log("Start ExecuteCancellationTokenSourceDemo");
            SimpleUniTaskWithToken(cts.Token);

            //�ȴ�2s��ȡ������
            Thread.Sleep(2000);
            Debug.Log("--ʱ�䵽��ȡ������");
            cts.Cancel();

            Debug.Log("End ExecuteCancellationTokenSourceDemo");
        }

        private async UniTask SimpleUniTaskWithToken(CancellationToken token)
        {
            //������ʹ��WithCancellation����token
            await UniTask.Delay(TimeSpan.FromSeconds(10000), ignoreTimeScale: false, cancellationToken: token);
            Debug.Log("End SimpleUniTaskWithToken");
        }
    }
}