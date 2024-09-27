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
    /// �����������ڿ���
    /// </summary>
    public class Demo4 : MonoBehaviour
    {
        CancellationTokenSource disableCancellation = new CancellationTokenSource();
        CancellationTokenSource destroyCancellation = new CancellationTokenSource();

        private void Start()
        {
            //�����й����н��ű����ã��ᴥ��disableCancellation

            Task.Run(async () =>
            {
                try
                {
                    //����token��cancel��������쳣
                    await SimpleUniTaskWithToken(disableCancellation.Token);
                }
                catch (Exception ex)
                {
                    Debug.Log("�쳣��" + ex.Message);
                }

                Debug.Log("����");
            });
        }

        private async UniTask SimpleUniTaskWithToken(CancellationToken token)
        {
            //������ʹ��WithCancellation����token
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