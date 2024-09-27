using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo11 : MonoBehaviour
    {
        /// <summary>
        /// ʹ��UniTask�еĴ���������������Task�еķ���
        /// </summary>
        private void CreateTaskDemo()
        {
            //��ʽ1
            UniTask.RunOnThreadPool(() => { });

            //��ʽ2
            UniTask.Create(async () => { await UniTask.Yield(); });

            //��ʽ3
            UniTask.Void(async () => { await UniTask.Yield(); });
        }
    }
}