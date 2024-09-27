using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Demo.Tools.UniTaskDemo
{
    public class Demo10 : MonoBehaviour
    {
        Action actEvent;
        UnityAction unityEvent; // UGUI特供

        public void ExecuteSubscribeEvent()
        {
            ////错误注册，因为这样写是 async void
            //actEvent += async () => { };
            //unityEvent += async () => { };

            // 这样是可以的: 通过lamada创建Action
            actEvent += UniTask.Action(async () => { await UniTask.Yield(); });
            unityEvent += UniTask.UnityAction(async () => { await UniTask.Yield(); });
        }
    }
}