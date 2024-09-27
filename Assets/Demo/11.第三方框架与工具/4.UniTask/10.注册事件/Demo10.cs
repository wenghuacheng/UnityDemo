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
        UnityAction unityEvent; // UGUI�ع�

        public void ExecuteSubscribeEvent()
        {
            ////����ע�ᣬ��Ϊ����д�� async void
            //actEvent += async () => { };
            //unityEvent += async () => { };

            // �����ǿ��Ե�: ͨ��lamada����Action
            actEvent += UniTask.Action(async () => { await UniTask.Yield(); });
            unityEvent += UniTask.UnityAction(async () => { await UniTask.Yield(); });
        }
    }
}