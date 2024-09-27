using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo12 : MonoBehaviour
    {
        private AsyncMessageBroker<string> broker = new AsyncMessageBroker<string>();

        private void Awake()
        {
            //订阅事件(只能单个，多个会报错)
            broker.Subscribe().Subscribe((msg) =>
            {
                Debug.Log("s1:" + msg);
            });
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 30), "Publish"))
            {
                Publish();
            }
        }

        private void Publish()
        {
            broker.Publish("One Message");
        }


    }
}