using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo14 : MonoBehaviour
    {
        [SerializeField] private Button button;

        private async void Start()
        {
            CancellationToken token = CancellationToken.None;

            //所有 uGUI 组件都实现***AsAsyncEnumerable了异步事件流的转换
            await TripleClick();
            await TripleClick2();
            await TripleClick3(token);
            Debug.Log("Start End");
        }

        async UniTask TripleClick()
        {
            // 默认情况下，使用了button.GetCancellationTokenOnDestroy 来管理异步生命周期
            await button.OnClickAsync();
            await button.OnClickAsync();
            await button.OnClickAsync();
            Debug.Log("[TripleClick]Three times clicked");
        }

        // 更高效的方法
        async UniTask TripleClick2()
        {
            using (var handler = button.GetAsyncClickEventHandler())
            {
                await handler.OnClickAsync();
                await handler.OnClickAsync();
                await handler.OnClickAsync();
                Debug.Log("[TripleClick2]Three times clicked");
            }
        }

        // 使用异步LINQ
        async UniTask TripleClick3(CancellationToken token)
        {
            await button.OnClickAsAsyncEnumerable().Take(3).LastAsync();
            Debug.Log("[TripleClick3]Three times clicked");
        }

    }
}