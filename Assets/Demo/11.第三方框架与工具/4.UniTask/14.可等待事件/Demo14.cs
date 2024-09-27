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

            //���� uGUI �����ʵ��***AsAsyncEnumerable���첽�¼�����ת��
            await TripleClick();
            await TripleClick2();
            await TripleClick3(token);
            Debug.Log("Start End");
        }

        async UniTask TripleClick()
        {
            // Ĭ������£�ʹ����button.GetCancellationTokenOnDestroy �������첽��������
            await button.OnClickAsync();
            await button.OnClickAsync();
            await button.OnClickAsync();
            Debug.Log("[TripleClick]Three times clicked");
        }

        // ����Ч�ķ���
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

        // ʹ���첽LINQ
        async UniTask TripleClick3(CancellationToken token)
        {
            await button.OnClickAsAsyncEnumerable().Take(3).LastAsync();
            Debug.Log("[TripleClick3]Three times clicked");
        }

    }
}