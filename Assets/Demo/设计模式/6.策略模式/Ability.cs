using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Patterns.Stratgy
{
    public enum AbilityEnum
    {
        ab1, ab2, ab3
    }

    /// <summary>
    /// 技能释放者
    /// </summary>
    public class AbilityRunner
    {
        List<IAbility> abilities = new List<IAbility>() { new Ability1(), new Ability2(), new Ability2() };

        public void Use(AbilityEnum type)
        {
            //策略模式可以解决大量ifelse之类的调用方式
            var ab = abilities.FirstOrDefault(p => p.Type == type);
            ab?.Do();
        }
    }

    #region 单个技能
    public interface IAbility
    {
        AbilityEnum Type { get; }

        void Do();
    }

    public class Ability1 : IAbility
    {
        public AbilityEnum Type => AbilityEnum.ab1;

        public void Do()
        {
            Debug.WriteLine("释放技能1");
        }
    }
    public class Ability2 : IAbility
    {
        public AbilityEnum Type => AbilityEnum.ab2;

        public void Do()
        {
            Debug.WriteLine("释放技能2");
        }
    }
    public class Ability3 : IAbility
    {
        public AbilityEnum Type => AbilityEnum.ab3;

        public void Do()
        {
            Debug.WriteLine("释放技能3");
        }
    }
    #endregion
}
