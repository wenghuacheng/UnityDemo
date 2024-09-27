using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.TestTools;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo : MonoBehaviour
    {
        private void Start()
        {
            //ExecuteWaitAllTask();

            //有问题
            //ExecuteWrapTask();

            //Task.Run(() => { ExecuteCancellationTokenSourceDemo(); });

            //Task.Run(async () =>
            //{
            //    int a = await ExecuteExceptionTaskDemo();
            //    Debug.Log(a);
            //});

            //Task.Run(() =>
            //{
            //    ExecuteTimeoutDemo();
            //});

            //Task.Run(() =>
            //{
            //    ExecuteTimeoutControllerDemo();
            //});

            Task.Run(() =>
            {

            });

            ExecuteProgressTaskDemo();

            Debug.Log("Start End");
        }

        private void FixedUpdate()
        {

        }



        #region 并行执行

        private async void ExecuteWaitAllTask()
        {
            Debug.Log("--开始执行并行任务");
            //任务同时完成后才会继续，可以用于加载所有资源，存档文件后继续执行后续操作
            await UniTask.WhenAll(SimpleUniTask(1), SimpleUniTask(2), SimpleUniTask(3));
            Debug.Log("--并行任务结束");
        }

        private async UniTask<string> SimpleUniTask(float delayTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), ignoreTimeScale: false);
            Debug.Log($"单个任务完成，模拟执行时间:{delayTime}s");
            return delayTime.ToString();
        }

        #endregion


        #region 封装回调
        private async void ExecuteWrapTask()
        {
            Debug.Log("--Start ExecuteWrapTask");
            await WrapByUniTaskCompletionSource();
            Debug.Log("--End ExecuteWrapTask");
        }

        private UniTask<int> WrapByUniTaskCompletionSource()
        {
            var utcs = new UniTaskCompletionSource<int>();

            // 当操作完成时，调用 utcs.TrySetResult();
            // 当操作失败时, 调用 utcs.TrySetException();
            // 当操作取消时, 调用 utcs.TrySetCanceled();

            utcs.TrySetResult(SimpleMethodReturnInt());

            return utcs.Task; //本质上就是返回了一个UniTask<int>
        }

        private int SimpleMethodReturnInt()
        {
            Debug.Log("--开始任务");
            Thread.Sleep(5000);
            Debug.Log("--结束任务");
            return 1;
        }

        #endregion

        #region 取消令牌
        private void ExecuteCancellationTokenSourceDemo()
        {
            var cts = new CancellationTokenSource();//创建令牌

            Debug.Log("Start ExecuteCancellationTokenSourceDemo");
            SimpleUniTaskWithToken(cts.Token);

            //等待2s后取消任务
            Thread.Sleep(2000);
            Debug.Log("--时间到，取消任务");
            cts.Cancel();

            Debug.Log("End ExecuteCancellationTokenSourceDemo");
        }

        private async UniTask SimpleUniTaskWithToken(CancellationToken token)
        {
            //还可以使用WithCancellation赋予token
            await UniTask.Delay(TimeSpan.FromSeconds(10000), ignoreTimeScale: false, cancellationToken: token);
            Debug.Log("End SimpleUniTaskWithToken");
        }

        #region 自定义令牌
        CancellationTokenSource disableCancellation = new CancellationTokenSource();
        CancellationTokenSource destroyCancellation = new CancellationTokenSource();

        private void OnEnable()
        {
            if (disableCancellation != null)
            {
                disableCancellation.Dispose();
            }
            disableCancellation = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            disableCancellation.Cancel();
        }

        private void OnDestroy()
        {
            destroyCancellation.Cancel();
            destroyCancellation.Dispose();
        }
        #endregion

        #region 超时

        private async void ExecuteTimeoutDemo()
        {
            var cts = new CancellationTokenSource();//创建令牌
            cts.CancelAfterSlim(TimeSpan.FromSeconds(3)); // 设置5s超时

            Debug.Log("Start ExecuteTimeoutDemo");
            try
            {
                await SimpleUniTaskWithToken(cts.Token);
            }
            catch (OperationCanceledException)
            {
                //超时会触发异常
                if (cts.IsCancellationRequested)
                {
                    UnityEngine.Debug.Log("Timeout.");
                }
                else if (cts.IsCancellationRequested)
                {
                    UnityEngine.Debug.Log("Cancel clicked.");
                }
            }

            Debug.Log("End ExecuteTimeoutDemo");
        }


        #endregion

        #region 超时优化
        private async void ExecuteTimeoutControllerDemo()
        {
            //使用TimeoutController 优化超时
            TimeoutController timeoutController = new TimeoutController(); // 复用timeoutController

            #region 【演示】可以与其他源一起使用
            //var clickCancelSource1 = new CancellationTokenSource();
            //var timeoutController1 = new TimeoutController(clickCancelSource1);
            #endregion

            Debug.Log("start ExecuteTimeoutControllerDemo");
            try
            {
                await SimpleUniTaskWithToken(timeoutController.Timeout(TimeSpan.FromSeconds(5)));
            }
            catch (OperationCanceledException)
            {
                if (timeoutController.IsTimeout())
                {
                    UnityEngine.Debug.Log("timeout");
                }
            }
            finally
            {
                timeoutController.Reset(); // 当await完成后调用Reset（停止超时计时器，并准备下一次复用）
            }

            Debug.Log("end ExecuteTimeoutControllerDemo");
        }

        #endregion

        #endregion

        #region 异常

        private async UniTask<int> ExecuteExceptionTaskDemo()
        {
            try
            {
                var x = await SimpleExceptionTask();
                return x * 2;
            }
            catch (Exception)
            //catch (Exception ex) when (!(ex is OperationCanceledException)) //过滤相关异常（这样此处就不会捕获异常，会将异常向上抛出）
            {
                return -1;
            }
        }

        public async UniTask<int> SimpleExceptionTask()
        {
            await UniTask.Yield();
            throw new OperationCanceledException();
        }

        #endregion

        #region 进度

        private async UniTask ExecuteProgressTaskDemo()
        {
            //需要使用Cysharp.Threading.Tasks.Progress命名空间下的，否则会导致每次更新进度都是new
            //还可以使用IProgress 接口
            var progress = Progress.Create<float>(x => Debug.Log(x));

            var request = await UnityWebRequest.Get("https://www.baidu.com")
                .SendWebRequest()
                .ToUniTask(progress: progress);

            //ps：需要在主线程。验证如何自定义进度
        }


        #endregion

        #region 空返回值
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
        #endregion

        #region 注册事件
        Action actEvent;
        UnityAction unityEvent; // UGUI特供

        public void ExecuteSubscribeEvent()
        {
            ////错误注册，因为这样写是 async void
            //actEvent += async () => { };
            //unityEvent += async () => { };

            // 这样是可以的: 通过lamada创建Action
            actEvent += UniTask.Action(async () => { await UniTask.Yield(); });
            unityEvent += UniTask.UnityAction(async () => { await UniTask.Yield(); });
        }

        #endregion

        #region 创建线程
        /// <summary>
        /// 使用UniTask中的创建方法，而不是Task中的方法
        /// </summary>
        private void CreateTaskDemo()
        {
            UniTask.RunOnThreadPool(() => { });
            UniTask.Create(async () => { await UniTask.Yield(); });
            UniTask.Void(async () => { await UniTask.Yield(); });
        }
        #endregion

        #region Channel

        //目前只支持多生产者、单消费者无界渠道

        //一个通用channel类
        public class AsyncMessageBroker<T> : IDisposable
        {
            Channel<T> channel;

            IConnectableUniTaskAsyncEnumerable<T> multicastSource;
            IDisposable connection;

            public AsyncMessageBroker()
            {
                //.Writer生产者
                //.Reader消费者

                channel = Channel.CreateSingleConsumerUnbounded<T>();//创建无界通道
                connection = multicastSource.Connect();//这个有啥用？

                //读取演示。使用TryRead、WaitToReadAsync、ReadAsync和Completion，ReadAllAsync来读取队列的消息                
                channel.Reader.ReadAsync();
            }

            /// <summary>
            /// 推送值
            /// </summary>
            /// <param name="value"></param>
            public void Publish(T value)
            {
                channel.Writer.TryWrite(value);
            }

            public IUniTaskAsyncEnumerable<T> Subscribe()
            {
                return multicastSource;
            }

            public void Dispose()
            {
                channel.Writer.TryComplete();
            }
        }

        #endregion

        /**
         对于检查（泄露的）UniTasks 很有用。您可以在Window -> UniTask Tracker中打开跟踪器窗口。
        UniTask.TextMeshPro, UniTask.DOTween, UniTask.Addressables
        AsyncEnumerable 和 Async LINQ  可等待事件
         */


    }
}