using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Patterns
{
    public class EventSender : MonoBehaviour
    {
        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 100), "≤‚ ‘"))
            {
                EventBus.Instance.Raise(new TestEvent(new Vector3(1, 0, 0)));
            }
        }
    }
}