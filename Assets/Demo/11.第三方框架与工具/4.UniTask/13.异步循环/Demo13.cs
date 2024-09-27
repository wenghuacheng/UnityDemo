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
             UniTaskAsyncEnumerable是类似的入口点Enumerable。除了标准查询运算符之外，还有其他 Unity 生成器，
             例如EveryUpdate、Timer、TimerFrame、Interval、IntervalFrame和EveryValueChanged。
            并且还添加了额外的 UniTask 原始查询运算符，
            如Append, Prepend, DistinctUntilChanged, ToHashSet, Buffer, CombineLatest, Do, Never, ForEachAsync, Pairwise, Publish, Queue, Return, SkipUntil, TakeUntil, SkipUntilCanceled, TakeUntilCanceled, TakeLast, Subscribe
             */

            RunUpdateEnumerable();

            //演示需要注释其他的演示方法
            RunButtonClickEnumerable();

            RunCustomEnumerable();
        }

        private void RunUpdateEnumerable()
        {
            Task.Run(async () =>
            {
                //系统迭代器，监听update（打印太多，隐藏）
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
                //监听鼠标点击
                await button1.OnClickAsAsyncEnumerable().Where((x, i) => i % 2 == 0).ForEachAsync(_ =>
                {
                    Debug.Log("偶数次点击才会显示");
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
                    Debug.Log("自定义迭代器：" + _);
                }, cancellationToken);
            });



        }


        #region 自定义迭代器


        // UniTaskAsyncEnumerable.Create 并用 `await writer.YieldAsync` 代替 `yield return`.
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