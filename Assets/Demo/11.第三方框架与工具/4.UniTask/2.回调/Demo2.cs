using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    /// <summary>
    /// 封装回调
    /// </summary>
    public class Demo2 : MonoBehaviour
    {
        void Start()
        {
            UniTask.Void(() =>
            {
                return ExecuteWrapTask();
            });
        }

        private async UniTaskVoid ExecuteWrapTask()
        {
            Debug.Log("--Start ExecuteWrapTask");
            var result = await WrapByUniTaskCompletionSource();
            Debug.Log("结果:" + result);
            Debug.Log("--End ExecuteWrapTask");
        }

        private UniTask<int> WrapByUniTaskCompletionSource()
        {
            var utcs = new UniTaskCompletionSource<int>();

            // 当操作完成时，调用 utcs.TrySetResult();
            // 当操作失败时, 调用 utcs.TrySetException();
            // 当操作取消时, 调用 utcs.TrySetCanceled();

            utcs.TrySetResult(SimpleMethodReturnInt());

            return utcs.Task; //本质上就是返回了一个UniTask<int>
        }

        private int SimpleMethodReturnInt()
        {
            Debug.Log("--开始任务");
            Thread.Sleep(5000);
            Debug.Log("--结束任务");
            return 999;
        }

    }
}