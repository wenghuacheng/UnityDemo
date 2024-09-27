using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo7 : MonoBehaviour
    {
        void Start()
        {
            Task.Run(async () =>
            {
                var result = await ExecuteExceptionTaskDemo();
                Debug.Log("����ֵ��" + result);
            });
        }

        private async UniTask<int> ExecuteExceptionTaskDemo()
        {
            try
            {
                var x = await SimpleExceptionTask();
                return x * 2;
            }
            catch (Exception)
            //catch (Exception ex) when (!(ex is OperationCanceledException)) //��������쳣�������˴��Ͳ��Ჶ���쳣���Ὣ�쳣�����׳���
            {
                return -1;
            }
        }

        public async UniTask<int> SimpleExceptionTask()
        {
            await UniTask.Yield();
            throw new OperationCanceledException();
        }
    }
}