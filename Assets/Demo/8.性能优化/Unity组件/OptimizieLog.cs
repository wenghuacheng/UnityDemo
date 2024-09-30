using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Demo.Optimizes
{
    /// <summary>
    /// 日志优化
    /// </summary>
    public class OptimizieLog
    {
        private void Method01()
        {
            //输出时使用条件编译，并且避免使用+号拼接字符串
#if UNITY_EDITOR
            Debug.LogError($"Error 1");
#endif


        }

    }


}
