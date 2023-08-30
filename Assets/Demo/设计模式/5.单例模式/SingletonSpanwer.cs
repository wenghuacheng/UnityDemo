using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.Patterns
{
    /// <summary>
    /// 单例模式生成器
    /// </summary>
    public class SingletonSpanwer : MonoBehaviour
    {
        //是否已经初始化过了
        private static bool isSpanwed = false;

        //挂在了单例对象的预制体
        [SerializeField] GameObject persistentObjectPerfab;

        private void Awake()
        {
            //已经初始化过了
            if (isSpanwed) return;

            //初始化预制体，预制体上的脚本就是单例
            GameObject gameObject = Instantiate(persistentObjectPerfab);
            DontDestroyOnLoad(gameObject);

            /**
             可以通过事件总线向对象发送消息
             */

            isSpanwed = true;
        }

    }

}