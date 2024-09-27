using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo13 : MonoBehaviour
    {
        [SerializeField] private Button button1;

        void Start()
        {
            /**
             UniTaskAsyncEnumerable�����Ƶ���ڵ�Enumerable�����˱�׼��ѯ�����֮�⣬�������� Unity ��������
             ����EveryUpdate��Timer��TimerFrame��Interval��IntervalFrame��EveryValueChanged��
            ���һ�����˶���� UniTask ԭʼ��ѯ�������
            ��Append, Prepend, DistinctUntilChanged, ToHashSet, Buffer, CombineLatest, Do, Never, ForEachAsync, Pairwise, Publish, Queue, Return, SkipUntil, TakeUntil, SkipUntilCanceled, TakeUntilCanceled, TakeLast, Subscribe
             */

            RunUpdateEnumerable();

            //��ʾ��Ҫע����������ʾ����
            RunButtonClickEnumerable();

            RunCustomEnumerable();
        }

        private void RunUpdateEnumerable()
        {
            Task.Run(async () =>
            {
                //ϵͳ������������update����ӡ̫�࣬���أ�
                var cts = new CancellationTokenSource();
                await UniTaskAsyncEnumerable.EveryUpdate().ForEachAsync(_ =>
                {
                    Debug.Log("Update() " + Time.frameCount);
                }, cts.Token);
            });
        }

        private void RunButtonClickEnumerable()
        {
            Task.Run(async () =>
            {
                //���������
                await button1.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).ForEachAsync(_ =>
                {
                    Debug.Log("ż���ε���Ż���ʾ");
                });
            });
        }

        private void RunCustomEnumerable()
        {
            CancellationToken cancellationToken = CancellationToken.None;
            Task.Run(async () =>
            {
                await MyEveryUpdate().ForEachAsync(_ =>
                {
                    Debug.Log("�Զ����������" + _);
                }, cancellationToken);
            });



        }


        #region �Զ��������


        // UniTaskAsyncEnumerable.Create ���� `await writer.YieldAsync` ���� `yield return`.
        public IUniTaskAsyncEnumerable<int> MyEveryUpdate()
        {
            return UniTaskAsyncEnumerable.Create<int>(async (writer, token) =>
            {
                var frameCount = 0;
                await UniTask.Yield();
                while (!token.IsCancellationRequested)
                {
                    await writer.YieldAsync(frameCount++); // instead of `yield return`
                    await UniTask.Yield();
                }
            });
        }
        #endregion
    }
}