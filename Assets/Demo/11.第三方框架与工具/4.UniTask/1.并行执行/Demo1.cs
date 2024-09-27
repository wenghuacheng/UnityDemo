using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    /// <summary>
    /// 并行执行
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
            Debug.Log("--开始执行并行任务");
            //任务同时完成后才会继续，可以用于加载所有资源，存档文件后继续执行后续操作
            await UniTask.WhenAll(SimpleUniTask(1), SimpleUniTask(2), SimpleUniTask(3));
            Debug.Log("--并行任务结束");
        }

        private async UniTask<string> SimpleUniTask(float delayTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), ignoreTimeScale: false);
            Debug.Log($"单个任务完成，模拟执行时间:{delayTime}s");
            return delayTime.ToString();
        }

    }
}