using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Demo.Tools.UniTaskDemo
{
    //Ŀǰֻ֧�ֶ������ߡ����������޽�����

    //һ��ͨ��channel��
    public class AsyncMessageBroker<T> : IDisposable
    {
        Channel<T> channel;

        IUniTaskAsyncEnumerable<T> multicastSource;
        IDisposable connection;

        public AsyncMessageBroker()
        {
            //.Writer������
            //.Reader������

            channel = Channel.CreateSingleConsumerUnbounded<T>();//�����޽�ͨ��
            multicastSource = channel.Reader.ReadAllAsync();
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
}