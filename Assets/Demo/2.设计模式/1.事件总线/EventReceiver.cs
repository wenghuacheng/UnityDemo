using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Patterns
{
    public class EventReceiver : MonoBehaviour, IEventReceiver<TestEvent>
    {
        private void Awake()
        {
            //ע���¼�������
            EventBus.Instance.Register(this);
        }

        public void OnEvnet(TestEvent @event)
        {
            this.transform.position = @event.position;
        }
    }
}
