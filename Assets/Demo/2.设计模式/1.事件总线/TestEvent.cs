using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Patterns
{
    /// <summary>
    /// ²âÊÔÊÂ¼þ
    /// </summary>
    public readonly struct TestEvent : IEvent
    {
        public readonly Vector3 position;

        public TestEvent(Vector3 position)
        {
            this.position = position;
        }
    }
}
