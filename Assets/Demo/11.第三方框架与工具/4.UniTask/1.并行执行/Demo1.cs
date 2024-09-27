using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    /// <summary>
    /// ����ִ��
    /// </summary>
    public class Demo1 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Task.Run(() =>
            {
                ExecuteWaitAllTask();
            });
        }

        private async void ExecuteWaitAllTask()
        {
            Debug.Log("--��ʼִ�в�������");
            //����ͬʱ��ɺ�Ż�������������ڼ���������Դ���浵�ļ������ִ�к�������
            await UniTask.WhenAll(SimpleUniTask(1), SimpleUniTask(2), SimpleUniTask(3));
            Debug.Log("--�����������");
        }

        private async UniTask<string> SimpleUniTask(float delayTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), ignoreTimeScale: false);
            Debug.Log($"����������ɣ�ģ��ִ��ʱ��:{delayTime}s");
            return delayTime.ToString();
        }

    }
}