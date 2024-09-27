using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    /// <summary>
    /// ��װ�ص�
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
            Debug.Log("���:" + result);
            Debug.Log("--End ExecuteWrapTask");
        }

        private UniTask<int> WrapByUniTaskCompletionSource()
        {
            var utcs = new UniTaskCompletionSource<int>();

            // ���������ʱ������ utcs.TrySetResult();
            // ������ʧ��ʱ, ���� utcs.TrySetException();
            // ������ȡ��ʱ, ���� utcs.TrySetCanceled();

            utcs.TrySetResult(SimpleMethodReturnInt());

            return utcs.Task; //�����Ͼ��Ƿ�����һ��UniTask<int>
        }

        private int SimpleMethodReturnInt()
        {
            Debug.Log("--��ʼ����");
            Thread.Sleep(5000);
            Debug.Log("--��������");
            return 999;
        }

    }
}