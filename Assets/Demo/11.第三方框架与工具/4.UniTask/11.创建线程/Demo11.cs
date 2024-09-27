using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo11 : MonoBehaviour
    {
        /// <summary>
        /// 使用UniTask中的创建方法，而不是Task中的方法
        /// </summary>
        private void CreateTaskDemo()
        {
            //方式1
            UniTask.RunOnThreadPool(() => { });

            //方式2
            UniTask.Create(async () => { await UniTask.Yield(); });

            //方式3
            UniTask.Void(async () => { await UniTask.Yield(); });
        }
    }
}