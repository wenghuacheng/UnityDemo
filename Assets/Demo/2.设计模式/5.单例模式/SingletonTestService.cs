using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Patterns
{
    /// <summary>
    /// 服务单例演示
    /// </summary>
    public class SingletonTestService
    {
        private static SingletonTestService _instance;

        public static SingletonTestService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SingletonTestService();
                return _instance;
            }
        }
    }
}
