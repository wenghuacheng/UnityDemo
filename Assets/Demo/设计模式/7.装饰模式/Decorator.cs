using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Patterns.Decorator
{
    /**
     * 装饰者模式通过包装之前的类达到给之前的功能附加新功能的效果
     * 包装者可以被多次包装多层
     * **/

    public interface IAbility
    {
        void Do();
    }

    /// <summary>
    /// 原始技能
    /// </summary>
    public class Ability1 : IAbility
    {
        public void Do()
        {
            //释放技能
        }
    }

    /// <summary>
    /// 包装后的技能
    /// </summary>
    public class DecoratorAbility1 : IAbility
    {
        private IAbility wrappedAbility;//原始技能

        public DecoratorAbility1(IAbility wrappedAbility)
        {
            this.wrappedAbility = wrappedAbility;
        }

        public void Do()
        {
            //todo：执行其他操作

            //执行基础操作
            wrappedAbility.Do();
        }
    }
}
