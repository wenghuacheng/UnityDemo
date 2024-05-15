using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Patterns
{
    #region �¼�����
    /// <summary>
    /// �¼�����
    /// </summary>
    public interface IEvent { }

    /// <summary>
    /// �¼������߽ӿ�
    /// </summary>
    public interface IBaseEventReceiver { }

    /// <summary>
    /// �¼�������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEventReceiver<T> : IBaseEventReceiver where T : struct, IEvent
    {
        void OnEvnet(T @event);
    }
    #endregion

    #region �¼�����
    public class EventBus
    {
        //ʹ�������ã���ֹ�����е����õ����ڴ�й¶
        private Dictionary<Type, List<WeakReference<IBaseEventReceiver>>> _receiver;
        //�¼������ϣ�����ڿ��ٲ��ҽ����߶���
        private Dictionary<int, WeakReference<IBaseEventReceiver>> _receiverHashToReference;

        public EventBus()
        {
            _receiver = new Dictionary<Type, List<WeakReference<IBaseEventReceiver>>>();
            _receiverHashToReference = new Dictionary<int, WeakReference<IBaseEventReceiver>>();
        }

        //����
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
        /// ע���¼�������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receiver"></param>
        public void Register<T>(IEventReceiver<T> receiver) where T : struct, IEvent
        {
            //�����¼�����������صĽ���������
            var eventType = typeof(T);
            if (!_receiver.ContainsKey(eventType))
                _receiver[eventType] = new List<WeakReference<IBaseEventReceiver>>();

            //�������߰�װΪ����������
            WeakReference<IBaseEventReceiver> reference = new WeakReference<IBaseEventReceiver>(receiver);
            _receiver[eventType].Add(reference);
            _receiverHashToReference[receiver.GetHashCode()] = reference;
        }

        /// <summary>
        /// �Ƴ��¼�������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="receiver"></param>
        public void UnRegister<T>(IEventReceiver<T> receiver) where T : struct, IEvent
        {
            var eventType = typeof(T);
            var hashCode = receiver.GetHashCode();
            if (!_receiver.ContainsKey(eventType) || !_receiverHashToReference.ContainsKey(hashCode))
                return;

            //ͨ��HashCode���ٻ�ȡ�����߶���
            var reference = _receiverHashToReference[hashCode];
            _receiver[eventType].Remove(reference);
            _receiverHashToReference.Remove(hashCode);
        }

        /// <summary>
        /// �����¼�
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
                //���Դ������ö����л�ȡ�����߶��󲢵���
                if (reference.TryGetTarget(out IBaseEventReceiver target))
                {
                    ((IEventReceiver<T>)target)?.OnEvnet(@event);
                }
            }
        }
    }

    #endregion
}