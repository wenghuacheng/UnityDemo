using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo8 : MonoBehaviour
    {
        void Start()
        {
            ExecuteProgressTaskDemo().Forget();
        }

        private async UniTaskVoid ExecuteProgressTaskDemo()
        {
            //需要使用Cysharp.Threading.Tasks.Progress命名空间下的，否则会导致每次更新进度都是new
            //还可以使用IProgress 接口
            var progress = Progress.Create<float>(x => Debug.Log(x));

            var request = await UnityWebRequest.Get("https://www.baidu.com")
                .SendWebRequest()
                .ToUniTask(progress: progress);

            Debug.Log(request.result);

            //ps：需要在主线程。验证如何自定义进度
        }
    }
}