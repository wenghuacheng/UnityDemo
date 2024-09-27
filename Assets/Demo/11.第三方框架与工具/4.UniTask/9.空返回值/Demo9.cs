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
            //PS:如果没有返回值需要使用UniTaskVoid，不要使用默认的void
            //也可以使用UniTask，但UniTaskVoid更高效
            //Start方法也可以改为UniTaskVoid返回值

            // do anything...
            await UniTask.Yield();
        }

        public void Caller()
        {
            //使用forget来让编译器没有波浪线
            FireAndForgetMethod().Forget();
        }
    }
}