using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// 管理器【用于容器注册等】
    /// </summary>
    public class CounterApp : Architecture<CounterApp>
    {
        protected override void Init()
        {
            //将需要的模型/组件/工具注册到架构中

            //注册Model
            this.RegisterModel<ICounterAppModel>(new CounterAppModel());

            //注册工具类【这里是存储类】
            this.RegisterUtility<Storage>(new Storage());

            //注册系统
            this.RegisterSystem<AchievementSystem>(new AchievementSystem());

        }
    }
}