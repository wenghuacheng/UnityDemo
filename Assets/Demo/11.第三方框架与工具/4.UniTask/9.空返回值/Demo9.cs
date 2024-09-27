using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo9 : MonoBehaviour
    {     
        public async UniTaskVoid FireAndForgetMethod()
        {
            //PS:���û�з���ֵ��Ҫʹ��UniTaskVoid����Ҫʹ��Ĭ�ϵ�void
            //Ҳ����ʹ��UniTask����UniTaskVoid����Ч
            //Start����Ҳ���Ը�ΪUniTaskVoid����ֵ

            // do anything...
            await UniTask.Yield();
        }

        public void Caller()
        {
            //ʹ��forget���ñ�����û�в�����
            FireAndForgetMethod().Forget();
        }
    }
}