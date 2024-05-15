using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Patterns
{
    #region 事件接收
    /// <summary>
    /// 事件对象
    /// </summary>
    public interface IEvent { }

    /// <summary>
    /// 事件接收者接口
    /// </summary>
    public interface IBaseEventReceiver { }

    /// <summary>
    /// 事件接收者
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventReceiver<T> : IBaseEventReceiver where T : struct, IEvent
    {
        void OnEvnet(T @event);
    }
    #endregion

    #region 事件总线
    public class EventBus
    {
        //使用弱引用，防止总线中的引用导致内存泄露
        private Dictionary<Type, List<WeakReference<IBaseEventReceiver>>> _receiver;
        //事件对象哈希表，用于快速查找接收者对象
        private Dictionary<int, WeakReference<IBaseEventReceiver>> _receiverHashToReference;

        public EventBus()
        {
            _receiver = new Dictionary<Type, List<WeakReference<IBaseEventReceiver>>>();
            _receiverHashToReference = new Dictionary<int, WeakReference<IBaseEventReceiver>>();
        }

        //单例
        public static EventBus _instance;
        public static EventBus Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventBus();
                return _instance;
            }
        }


        /// <summary>
        /// 注册事件接收者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receiver"></param>
        public void Register<T>(IEventReceiver<T> receiver) where T : struct, IEvent
        {
            //基于事件类型生成相关的接收者容器
            var eventType = typeof(T);
            if (!_receiver.ContainsKey(eventType))
                _receiver[eventType] = new List<WeakReference<IBaseEventReceiver>>();

            //将接收者包装为弱引用类型
            WeakReference<IBaseEventReceiver> reference = new WeakReference<IBaseEventReceiver>(receiver);
            _receiver[eventType].Add(reference);
            _receiverHashToReference[receiver.GetHashCode()] = reference;
        }

        /// <summary>
        /// 移除事件接收者
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receiver"></param>
        public void UnRegister<T>(IEventReceiver<T> receiver) where T : struct, IEvent
        {
            var eventType = typeof(T);
            var hashCode = receiver.GetHashCode();
            if (!_receiver.ContainsKey(eventType) || !_receiverHashToReference.ContainsKey(hashCode))
                return;

            //通过HashCode快速获取接收者对象
            var reference = _receiverHashToReference[hashCode];
            _receiver[eventType].Remove(reference);
            _receiverHashToReference.Remove(hashCode);
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="event"></param>
        public void Raise<T>(T @event) where T : struct, IEvent
        {
            var eventType = typeof(T);
            if (!_receiver.ContainsKey(eventType))
                return;

            Debug.Log(_receiver[eventType].Count);
            foreach (var reference in _receiver[eventType])
            {
                //尝试从弱引用对象中获取接收者对象并调用
                if (reference.TryGetTarget(out IBaseEventReceiver target))
                {
                    ((IEventReceiver<T>)target)?.OnEvnet(@event);
                }
            }
        }
    }

    #endregion
}