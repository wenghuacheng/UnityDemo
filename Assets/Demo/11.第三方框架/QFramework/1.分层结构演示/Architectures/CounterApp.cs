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
            Debug.Log("初始化CounterApp");

            //注册Model【这里注册的是接口类型则获取时使用接口类型，实现类则直接获取实现类】
            this.RegisterModel<CounterAppModel>(new CounterAppModel());

            //注册工具类【这里是存储类】
            this.RegisterUtility<Storage>(new Storage());

            //注册系统
            this.RegisterSystem<AchievementSystem>(new AchievementSystem());

        }
    }
}