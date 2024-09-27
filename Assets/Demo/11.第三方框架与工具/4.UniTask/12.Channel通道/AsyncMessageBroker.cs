using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Demo.Tools.UniTaskDemo
{
    //目前只支持多生产者、单消费者无界渠道

    //一个通用channel类
    public class AsyncMessageBroker<T> : IDisposable
    {
        Channel<T> channel;

        IUniTaskAsyncEnumerable<T> multicastSource;
        IDisposable connection;

        public AsyncMessageBroker()
        {
            //.Writer生产者
            //.Reader消费者

            channel = Channel.CreateSingleConsumerUnbounded<T>();//创建无界通道
            multicastSource = channel.Reader.ReadAllAsync();
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
}