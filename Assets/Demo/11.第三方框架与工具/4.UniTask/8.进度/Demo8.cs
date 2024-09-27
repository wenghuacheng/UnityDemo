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
            //��Ҫʹ��Cysharp.Threading.Tasks.Progress�����ռ��µģ�����ᵼ��ÿ�θ��½��ȶ���new
            //������ʹ��IProgress �ӿ�
            var progress = Progress.Create<float>(x => Debug.Log(x));

            var request = await UnityWebRequest.Get("https://www.baidu.com")
                .SendWebRequest()
                .ToUniTask(progress: progress);

            Debug.Log(request.result);

            //ps����Ҫ�����̡߳���֤����Զ������
        }
    }
}