using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Demo.Layers
{
    /// <summary>
    /// ����������������ע��ȡ�
    /// </summary>
    public class CounterApp : Architecture<CounterApp>
    {
        protected override void Init()
        {
            //����Ҫ��ģ��/���/����ע�ᵽ�ܹ���
            Debug.Log("��ʼ��CounterApp");

            //ע��Model������ע����ǽӿ��������ȡʱʹ�ýӿ����ͣ�ʵ������ֱ�ӻ�ȡʵ���ࡿ
            this.RegisterModel<CounterAppModel>(new CounterAppModel());

            //ע�Ṥ���ࡾ�����Ǵ洢�ࡿ
            this.RegisterUtility<Storage>(new Storage());

            //ע��ϵͳ
            this.RegisterSystem<AchievementSystem>(new AchievementSystem());

        }
    }
}