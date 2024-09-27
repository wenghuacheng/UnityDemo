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

            //������
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



        #region ����ִ��

        private async void ExecuteWaitAllTask()
        {
            Debug.Log("--��ʼִ�в�������");
            //����ͬʱ��ɺ�Ż�������������ڼ���������Դ���浵�ļ������ִ�к�������
            await UniTask.WhenAll(SimpleUniTask(1), SimpleUniTask(2), SimpleUniTask(3));
            Debug.Log("--�����������");
        }

        private async UniTask<string> SimpleUniTask(float delayTime)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayTime), ignoreTimeScale: false);
            Debug.Log($"����������ɣ�ģ��ִ��ʱ��:{delayTime}s");
            return delayTime.ToString();
        }

        #endregion


        #region ��װ�ص�
        private async void ExecuteWrapTask()
        {
            Debug.Log("--Start ExecuteWrapTask");
            await WrapByUniTaskCompletionSource();
            Debug.Log("--End ExecuteWrapTask");
        }

        private UniTask<int> WrapByUniTaskCompletionSource()
        {
            var utcs = new UniTaskCompletionSource<int>();

            // ���������ʱ������ utcs.TrySetResult();
            // ������ʧ��ʱ, ���� utcs.TrySetException();
            // ������ȡ��ʱ, ���� utcs.TrySetCanceled();

            utcs.TrySetResult(SimpleMethodReturnInt());

            return utcs.Task; //�����Ͼ��Ƿ�����һ��UniTask<int>
        }

        private int SimpleMethodReturnInt()
        {
            Debug.Log("--��ʼ����");
            Thread.Sleep(5000);
            Debug.Log("--��������");
            return 1;
        }

        #endregion

        #region ȡ������
        private void ExecuteCancellationTokenSourceDemo()
        {
            var cts = new CancellationTokenSource();//��������

            Debug.Log("Start ExecuteCancellationTokenSourceDemo");
            SimpleUniTaskWithToken(cts.Token);

            //�ȴ�2s��ȡ������
            Thread.Sleep(2000);
            Debug.Log("--ʱ�䵽��ȡ������");
            cts.Cancel();

            Debug.Log("End ExecuteCancellationTokenSourceDemo");
        }

        private async UniTask SimpleUniTaskWithToken(CancellationToken token)
        {
            //������ʹ��WithCancellation����token
            await UniTask.Delay(TimeSpan.FromSeconds(10000), ignoreTimeScale: false, cancellationToken: token);
            Debug.Log("End SimpleUniTaskWithToken");
        }

        #region �Զ�������
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

        #region ��ʱ

        private async void ExecuteTimeoutDemo()
        {
            var cts = new CancellationTokenSource();//��������
            cts.CancelAfterSlim(TimeSpan.FromSeconds(3)); // ����5s��ʱ

            Debug.Log("Start ExecuteTimeoutDemo");
            try
            {
                await SimpleUniTaskWithToken(cts.Token);
            }
            catch (OperationCanceledException)
            {
                //��ʱ�ᴥ���쳣
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

        #region ��ʱ�Ż�
        private async void ExecuteTimeoutControllerDemo()
        {
            //ʹ��TimeoutController �Ż���ʱ
            TimeoutController timeoutController = new TimeoutController(); // ����timeoutController

            #region ����ʾ������������Դһ��ʹ��
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
                timeoutController.Reset(); // ��await��ɺ����Reset��ֹͣ��ʱ��ʱ������׼����һ�θ��ã�
            }

            Debug.Log("end ExecuteTimeoutControllerDemo");
        }

        #endregion

        #endregion

        #region �쳣

        private async UniTask<int> ExecuteExceptionTaskDemo()
        {
            try
            {
                var x = await SimpleExceptionTask();
                return x * 2;
            }
            catch (Exception)
            //catch (Exception ex) when (!(ex is OperationCanceledException)) //��������쳣�������˴��Ͳ��Ჶ���쳣���Ὣ�쳣�����׳���
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

        #region ����

        private async UniTask ExecuteProgressTaskDemo()
        {
            //��Ҫʹ��Cysharp.Threading.Tasks.Progress�����ռ��µģ�����ᵼ��ÿ�θ��½��ȶ���new
            //������ʹ��IProgress �ӿ�
            var progress = Progress.Create<float>(x => Debug.Log(x));

            var request = await UnityWebRequest.Get("https://www.baidu.com")
                .SendWebRequest()
                .ToUniTask(progress: progress);

            //ps����Ҫ�����̡߳���֤����Զ������
        }


        #endregion

        #region �շ���ֵ
        public async UniTaskVoid FireAndForgetMethod()
        {
            //PS:���û�з���ֵ��Ҫʹ��UniTaskVoid����Ҫʹ��Ĭ�ϵ�void
            //Ҳ����ʹ��UniTask����UniTaskVoid����Ч
            //Start����Ҳ���Ը�ΪUniTaskVoid����ֵ

            // do anything...
            await UniTask.Yield();
        }

        public void Caller()
        {
            //ʹ��forget���ñ�����û�в�����
            FireAndForgetMethod().Forget();
        }
        #endregion

        #region ע���¼�
        Action actEvent;
        UnityAction unityEvent; // UGUI�ع�

        public void ExecuteSubscribeEvent()
        {
            ////����ע�ᣬ��Ϊ����д�� async void
            //actEvent += async () => { };
            //unityEvent += async () => { };

            // �����ǿ��Ե�: ͨ��lamada����Action
            actEvent += UniTask.Action(async () => { await UniTask.Yield(); });
            unityEvent += UniTask.UnityAction(async () => { await UniTask.Yield(); });
        }

        #endregion

        #region �����߳�
        /// <summary>
        /// ʹ��UniTask�еĴ���������������Task�еķ���
        /// </summary>
        private void CreateTaskDemo()
        {
            UniTask.RunOnThreadPool(() => { });
            UniTask.Create(async () => { await UniTask.Yield(); });
            UniTask.Void(async () => { await UniTask.Yield(); });
        }
        #endregion

        #region Channel

        //Ŀǰֻ֧�ֶ������ߡ����������޽�����

        //һ��ͨ��channel��
        public class AsyncMessageBroker<T> : IDisposable
        {
            Channel<T> channel;

            IConnectableUniTaskAsyncEnumerable<T> multicastSource;
            IDisposable connection;

            public AsyncMessageBroker()
            {
                //.Writer������
                //.Reader������

                channel = Channel.CreateSingleConsumerUnbounded<T>();//�����޽�ͨ��
                connection = multicastSource.Connect();//�����ɶ�ã�

                //��ȡ��ʾ��ʹ��TryRead��WaitToReadAsync��ReadAsync��Completion��ReadAllAsync����ȡ���е���Ϣ                
                channel.Reader.ReadAsync();
            }

            /// <summary>
            /// ����ֵ
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
         ���ڼ�飨й¶�ģ�UniTasks �����á���������Window -> UniTask Tracker�д򿪸��������ڡ�
        UniTask.TextMeshPro, UniTask.DOTween, UniTask.Addressables
        AsyncEnumerable �� Async LINQ  �ɵȴ��¼�
         */


    }
}