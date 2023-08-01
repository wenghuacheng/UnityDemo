using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReceiver : MonoBehaviour, IEventReceiver<TestEvent>
{
    private void Awake()
    {
        //注册事件接收器
        EventBus.Instance.Register(this);
    }

    public void OnEvnet(TestEvent @event)
    {
        this.transform.position = @event.position;
    }
}
